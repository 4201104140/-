// The MIT License (MIT)
// 
// Copyright (c) 2015-2016 Microsoft
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace Microsoft.IO
{
    using System;
#if NETCOREAPP2_1 || NETSTANDARD2_1
    using System.Buffers;
#endif
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// MemoryStream implementation that deals with pooling and managing memory streams which use potentially large
    /// buffers.
    /// </summary>
    /// <remarks>
    /// This class works in tandem with the <see cref="RecyclableMemoryStreamManager"/> to supply <c>MemoryStream</c>-derived
    /// objects to callers, while avoiding these specific problems:
    /// <list type="number">
    /// <item>
    /// <term>LOH allocations</term>
    /// <description>Since all large buffers are pooled, they will never incur a Gen2 GC</description>
    /// </item>
    /// <item>
    /// <term>Memory waste</term><description>A standard memory stream doubles its size when it runs out of room. This
    /// leads to continual memory growth as each stream approaches the maximum allowed size.</description>
    /// </item>
    /// <item>
    /// <term>Memory copying</term>
    /// <description>Each time a <c>MemoryStream</c> grows, all the bytes are copied into new buffers.
    /// This implementation only copies the bytes when <see cref="GetBuffer"/> is called.</description>
    /// </item>
    /// <item>
    /// <term>Memory fragmentation</term>
    /// <description>By using homogeneous buffer sizes, it ensures that blocks of memory
    /// can be easily reused.
    /// </description>
    /// </item>
    /// </list>
    /// <para>
    /// The stream is implemented on top of a series of uniformly-sized blocks. As the stream's length grows,
    /// additional blocks are retrieved from the memory manager. It is these blocks that are pooled, not the stream
    /// object itself.
    /// </para>
    /// <para>
    /// The biggest wrinkle in this implementation is when <see cref="GetBuffer"/> is called. This requires a single 
    /// contiguous buffer. If only a single block is in use, then that block is returned. If multiple blocks 
    /// are in use, we retrieve a larger buffer from the memory manager. These large buffers are also pooled, 
    /// split by size--they are multiples/exponentials of a chunk size (1 MB by default).
    /// </para>
    /// <para>
    /// Once a large buffer is assigned to the stream the small blocks are NEVER again used for this stream. All operations take place on the 
    /// large buffer. The large buffer can be replaced by a larger buffer from the pool as needed. All blocks and large buffers 
    /// are maintained in the stream until the stream is disposed (unless AggressiveBufferReturn is enabled in the stream manager).
    /// </para>
    /// <para>
    /// A further wrinkle is what happens when the stream is longer than the maximum allowable array length under .NET. This is allowed
    /// when only blocks are in use, and only the Read/Write APIs are used. Once a stream grows to this size, any attempt to convert it
    /// to a single buffer will result in an exception. Similarly, if a stream is already converted to use a single larger buffer, then
    /// it cannot grow beyond the limits of the maximum allowable array size.
    /// </para>
    /// </remarks>
    public sealed class RecyclableMemoryStream : MemoryStream
    {
        private static readonly byte[] emptyArray = new byte[0];

        /// <summary>
        /// All of these blocks must be the same size
        /// </summary>
        private readonly List<byte[]> blocks = new List<byte[]>(1);

        private readonly Guid id;

        private readonly RecyclableMemoryStreamManager memoryManager;

        private readonly string tag;

        /// <summary>
        /// This list is used to store buffers once they're replaced by something larger.
        /// This is for the cases where you have users of this class that may hold onto the buffers longer
        /// than they should and you want to prevent race conditions which could corrupt the data.
        /// </summary>
        private List<byte[]> dirtyBuffers;

        private bool disposed;

        /// <summary>
        /// This is only set by GetBuffer() if the necessary buffer is larger than a single block size, or on
        /// construction if the caller immediately requests a single large buffer.
        /// </summary>
        /// <remarks>If this field is non-null, it contains the concatenation of the bytes found in the individual
        /// blocks. Once it is created, this (or a larger) largeBuffer will be used for the life of the stream.
        /// </remarks>
        private byte[] largeBuffer;

        /// <summary>
        /// Unique identifier for this stream across its entire lifetime
        /// </summary>
        /// <exception cref="ObjectDisposedException">Object has been disposed</exception>
        internal Guid Id
        {
            get
            {
                this.CheckDisposed();
                return this.id;
            }
        }

        /// <summary>
        /// A temporary identifier for the current usage of this stream.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Object has been disposed</exception>
        internal string Tag
        {
            get
            {
                this.CheckDisposed();
                return this.tag;
            }
        }

        /// <summary>
        /// Gets the memory manager being used by this stream.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Object has been disposed</exception>
        internal RecyclableMemoryStreamManager MemoryManager
        {
            get
            {
                this.CheckDisposed();
                return this.memoryManager;
            }
        }

        /// <summary>
        /// Callstack of the constructor. It is only set if <see cref="RecyclableMemoryStreamManager.GenerateCallStacks"/> is true,
        /// which should only be in debugging situations.
        /// </summary>
        internal string AllocationStack { get; }

        /// <summary>
        /// Callstack of the <see cref="Dispose(bool)"/> call. It is only set if <see cref="RecyclableMemoryStreamManager.GenerateCallStacks"/> is true,
        /// which should only be in debugging situations.
        /// </summary>
        internal string DisposeStack { get; private set; }

        #region Constructors
        /// <summary>
        /// Allocate a new RecyclableMemoryStream object.
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager)
            : this(memoryManager, Guid.NewGuid(), null, 0, null) { }

        /// <summary>
        /// Allocate a new <c>RecyclableMemoryStream</c> object.
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        /// <param name="id">A unique identifier which can be used to trace usages of the stream.</param>
        public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, Guid id)
            : this(memoryManager, id, null, 0, null) { }

        /// <summary>
        /// Allocate a new <c>RecyclableMemoryStream</c> object
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        /// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
        public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, string tag)
            : this(memoryManager, Guid.NewGuid(), tag, 0, null) { }

        /// <summary>
        /// Allocate a new <c>RecyclableMemoryStream</c> object
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        /// <param name="id">A unique identifier which can be used to trace usages of the stream.</param>
        /// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
        public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, Guid id, string tag)
            : this(memoryManager, id, tag, 0, null) { }

        /// <summary>
        /// Allocate a new <c>RecyclableMemoryStream</c> object
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        /// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
        /// <param name="requestedSize">The initial requested size to prevent future allocations</param>
        public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, string tag, int requestedSize)
            : this(memoryManager, Guid.NewGuid(), tag, requestedSize, null) { }

        /// <summary>
        /// Allocate a new <c>RecyclableMemoryStream</c> object
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        /// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
        /// <param name="requestedSize">The initial requested size to prevent future allocations</param>
        public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, string tag, long requestedSize)
            : this(memoryManager, Guid.NewGuid(), tag, requestedSize, null) { }

        /// <summary>
        /// Allocate a new <c>RecyclableMemoryStream</c> object
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        /// <param name="id">A unique identifier which can be used to trace usages of the stream.</param>
        /// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
        /// <param name="requestedSize">The initial requested size to prevent future allocations</param>
        public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, Guid id, string tag, int requestedSize)
            : this(memoryManager, id, tag, requestedSize, null) { }

        /// <summary>
        /// Allocate a new <c>RecyclableMemoryStream</c> object
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        /// <param name="id">A unique identifier which can be used to trace usages of the stream.</param>
        /// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
        /// <param name="requestedSize">The initial requested size to prevent future allocations</param>
        public RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, Guid id, string tag, long requestedSize)
            : this(memoryManager, id, tag, requestedSize, null) { }

        /// <summary>
        /// Allocate a new <c>RecyclableMemoryStream</c> object
        /// </summary>
        /// <param name="memoryManager">The memory manager</param>
        /// <param name="id">A unique identifier which can be used to trace usages of the stream.</param>
        /// <param name="tag">A string identifying this stream for logging and debugging purposes</param>
        /// <param name="requestedSize">The initial requested size to prevent future allocations</param>
        /// <param name="initialLargeBuffer">An initial buffer to use. This buffer will be owned by the stream and returned to the memory manager upon Dispose.</param>
        internal RecyclableMemoryStream(RecyclableMemoryStreamManager memoryManager, Guid id, string tag, long requestedSize, byte[] initialLargeBuffer)
            : base(emptyArray)
        {
            this.memoryManager = memoryManager;
            this.id = id;
            this.tag = tag;

            var actualRequestedSize = requestedSize;
            if (actualRequestedSize < this.memoryManager.BlockSize)
            {
                actualRequestedSize = this.memoryManager.BlockSize;
            }

            if (initialLargeBuffer == null)
            {
                this.EnsureCapacity(actualRequestedSize);
            }
            else
            {
                this.largeBuffer = initialLargeBuffer;
            }

            if (this.memoryManager.GenerateCallStacks)
            {
                this.AllocationStack = Environment.StackTrace;
            }

            this.memoryManager.ReportStreamCreated(this.id, this.tag, requestedSize, actualRequestedSize);
        }
        #endregion

        #region Dispose and Finalize
        /// <summary>
        /// The finalizer will be called when a stream is not disposed properly. 
        /// </summary>
        /// <remarks>Failing to dispose indicates a bug in the code using streams. Care should be taken to properly account for stream lifetime.</remarks>
        ~RecyclableMemoryStream()
        {
            this.Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                string doubleDisposeStack = null;
                if (this.memoryManager.GenerateCallStacks)
                {
                    doubleDisposeStack = Environment.StackTrace;
                }

                this.memoryManager.ReportStreamDoubleDisposed(this.id, this.tag, this.AllocationStack, this.DisposeStack, doubleDisposeStack);
                return;
            }

            this.disposed = true;

            if (this.memoryManager.GenerateCallStacks)
            {
                this.DisposeStack = Environment.StackTrace;
            }

            this.memoryManager.ReportStreamDisposed(this.id, this.tag, this.AllocationStack, this.DisposeStack);

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
            else
            {
                // We're being finalized.
                this.memoryManager.ReportStreamFinalized(this.id, this.tag, this.AllocationStack);

                if (AppDomain.CurrentDomain.IsFinalizingForUnload())
                {
                    // If we're being finalized because of a shutdown, don't go any further.
                    // We have no idea what's already been cleaned up. Triggering events may cause
                    // a crash.
                    base.Dispose(disposing);
                    return;
                }

            }

            this.memoryManager.ReportStreamLength(this.length);

            if (this.largeBuffer != null)
            {
                
            }

            this.memoryManager.ReturnBlocks(this.blocks, this.id, this.tag);
            this.blocks.Clear();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Equivalent to <c>Dispose</c>
        /// </summary>
        public override void Close()
        {
            this.Dispose(true);
        }
        #endregion

        #region MemoryStream overrides

        private long length;
        #endregion

        #region Helper Methods
        private bool Disposed => this.Disposed;

        [MethodImpl((MethodImplOptions)256)]
        private void CheckDisposed()
        {
            if (this.Disposed)
            {
                this.ThrowDisposedException();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowDisposedException()
        {
            throw new ObjectDisposedException($"The stream with Id {this.id} and Tag {this.tag} is disposed.");
        }

        private int InternalRead(byte[] buffer, int offset, int count, long fromPosition)
        {
            if (this.length - fromPosition <= 0)
            {
                return 0;
            }

            int amountToCopy;

            if (this.largeBuffer == null)
            {
                
            }
            amountToCopy = (int)Math.Min((long)count, this.length - fromPosition);

            return amountToCopy;
        }


        private void EnsureCapacity(long newCapacity)
        {
            if (newCapacity > this.memoryManager.MaximumStreamCapacity && this.memoryManager.MaximumStreamCapacity > 0)
            {
                this.memoryManager.ReportStreamOverCapacity(this.id, this.tag, newCapacity, this.AllocationStack);
                throw new InvalidOperationException("Requested capacity is too large: " + newCapacity + ". Limit is " +
                                                    this.memoryManager.MaximumStreamCapacity);
            }

            if (this.largeBuffer != null)
            {
                if (newCapacity > this.largeBuffer.Length)
                {
                    var newBuffer = this.memoryManager.GetLargeBuffer(newCapacity, this.id, this.tag);
                    
                }
            }
        }

        private void ReleaseLargeBuffer()
        {
            if (this.memoryManager.AggressiveBufferReturn)
            {
                this.memoryManager.Return
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AssertLengthIsSmall()
        {
            Debug.Assert(this.length <= Int32.MaxValue, "this.length was assumed to be <= Int32.MaxValue, but was larger.");
        }
        #endregion
    }
}