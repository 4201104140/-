
namespace Microsoft.IO
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class RecyclableMemoryStream : MemoryStream, IBufferWriter<byte>
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

        /// <summary>
        /// Returns the memory used by this stream back to the pool.
        /// </summary>
        /// <param name="disposing">Whether we're disposing (true), or being called by the finalizer (false)</param>
        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                string doubleDisposeStack = null;
                if (this.memoryManager.GenerateCallStacks)
                {
                    doubleDisposeStack = Environment.StackTrace;
                }

                return;
            }

            this.disposed = true;

            if (this.memoryManager.GenerateCallStacks)
            {
                this.DisposeStack = Environment.StackTrace;
            }

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
            else
            {


                if (AppDomain.CurrentDomain.IsFinalizingForUnload())
                {
                    // If we're being finalized because of a shutdown, don't go any further.
                    // We have no idea what's already been cleaned up. Triggering events may cause
                    // a crash.
                    base.Dispose(disposing);
                    return;
                }
            }

            if (this.largeBuffer != null)
            {
                
            }

            if (this.dirtyBuffers != null)
            {

            }

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


        private byte[] bufferWriterTempBuffer;

        /// <summary>
        /// Notifies the stream that <paramref name="count"/> bytes were written to the buffer returned by <see cref="Get"/>
        /// </summary>
        /// <param name="count"></param>
        public void Advance(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} must be non-negative.");
            }

            //byte[] buffer = this.buff
        }


        /// <inheritdoc/>
        /// <remarks>
        /// IMPORTANT: Calling Write(), GetBuffer(), TryGetBuffer(), Seek(), GetLength(), Advance(),
        /// or setting Position after calling GetMemory() invalidates the memory.
        /// </remarks>
        public Memory<byte> GetMemory(int sizeHint = 0) => this.GetWritableBuffer(sizeHint);

        /// <inheritdoc/>
        /// <remarks>
        /// IMPORTANT: Calling Write(), GetBuffer(), TryGetBuffer(), Seek(), GetLength(), Advance(),
        /// or setting Position after calling GetMemory() invalidates the memory.
        /// </remarks>
        public Span<byte> GetSpan(int sizeHint = 0) => this.GetWritableBuffer(sizeHint);

        private ArraySegment<byte> GetWritableBuffer(int minimumBufferSize)
        {
            this.CheckDisposed();
            if (minimumBufferSize < 0)
            {
                throw new ArgumentOutOfRangeException("sizeHint", $"sizeHint must be non-negative");
            }

            if (minimumBufferSize == 0)
            {
                minimumBufferSize = 1;
            }

            return new ArraySegment<byte>(this.bufferWriterTempBuffer);
        }
        #endregion

        #region Helper Methods
        private bool Disposed => this.disposed;

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


        private void EnsureCapacity(long newCapacity)
        {
            if (newCapacity > this.memoryManager.MaximumStreamCapacity && this.memoryManager.MaximumStreamCapacity > 0)
            {
                //this.memoryManager.
                throw new OutOfMemoryException(
                    "Requested capacity is too large: " + newCapacity.ToString(CultureInfo.InvariantCulture) +
                    ". Limit is " + this.memoryManager.MaximumStreamCapacity.ToString(CultureInfo.InvariantCulture));
            }

            if (this.largeBuffer != null)
            {
                if (newCapacity > this.largeBuffer.Length)
                {
                    
                }
            }
        }
        #endregion
    }
}