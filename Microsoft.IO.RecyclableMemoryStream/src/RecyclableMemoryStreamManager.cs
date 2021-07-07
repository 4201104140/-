

namespace Microsoft.IO
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    /// Manages pools of  objects.
    /// </summary>
    public partial class RecyclableMemoryStreamManager
    {
        /// <summary>
        /// Maximum length of a single array.
        /// </summary>
        /// <remarks>See documentation at https://docs.microsoft.com/dotnet/api/system.array?view=netcore-3.1
        /// </remarks>
        internal const int MaxArrayLength = 0X7FFFFFC7;

        /// <summary>
        /// Default block size, in bytes
        /// </summary>
        public const int DefaultBlockSize = 128 * 1024;
        /// <summary>
        /// Default large buffer multiple, in bytes
        /// </summary>
        public const int DefaultLargeBufferMultiple = 1024 * 1024;
        /// <summary>
        /// Default maximum buffer size, in bytes
        /// </summary>
        public const int DefaultMaximumBufferSize = 128 * 1024 * 1024;

        // 0 to indicate unbounded
        private const long DefaultMaxSmallPoolFreeBytes = 0L;
        private const long DefaultMaxLargePoolFreeBytes = 0L;

        private readonly long[] largeBufferFreeSize;
        private readonly long[] largeBufferInUseSize;


        private readonly ConcurrentStack<byte[]>[] largePools;

        private readonly ConcurrentStack<byte[]> smallPool;

        private long smallPoolFreeSize;
        private long smallPoolInUseSize;


        /// <summary>
        /// Initializes the memory manager with the given block requiredSize. This pool may have unbounded growth unless you modify <see cref="MaximumFreeSmallPoolBytes"/> and <see cref="MaximumFreeLargePoolBytes"/>.
        /// </summary>
        /// <param name="blockSize">Size of each block that is pooled. Must be > 0.</param>
        /// <param name="largeBufferMultiple">Each large buffer will be a multiple/exponential of this value.</param>
        /// <param name="maximumBufferSize">Buffers larger than this are not pooled</param>
        /// <param name="useExponentialLargeBuffer">Switch to exponential large buffer allocation strategy</param>
        /// <exception cref="ArgumentOutOfRangeException">blockSize is not a positive number, or largeBufferMultiple is not a positive number, or maximumBufferSize is less than blockSize.</exception>
        /// <exception cref="ArgumentException">maximumBufferSize is not a multiple/exponential of largeBufferMultiple</exception>
        public RecyclableMemoryStreamManager(int blockSize, int largeBufferMultiple, int maximumBufferSize, bool useExponentialLargeBuffer)
            : this(blockSize, largeBufferMultiple, maximumBufferSize, useExponentialLargeBuffer, DefaultMaxSmallPoolFreeBytes, DefaultMaxLargePoolFreeBytes)
        {

        }

        /// <summary>
        /// Initializes the memory manager with the given block requiredSize.
        /// </summary>
        /// <param name="blockSize">Size of each block that is pooled. Must be > 0.</param>
        /// <param name="largeBufferMultiple">Each large buffer will be a multiple/exponential of this value.</param>
        /// <param name="maximumBufferSize">Buffers larger than this are not pooled</param>
        /// <param name="useExponentialLargeBuffer">Switch to exponential large buffer allocation strategy</param>
        /// <param name="maximumSmallPoolFreeBytes">Maximum number of bytes to keep available in the small pool before future buffers get dropped for garbage collection</param>
        /// <param name="maximumLargePoolFreeBytes">Maximum number of bytes to keep available in the large pool before future buffers get dropped for garbage collection</param>
        /// <exception cref="ArgumentOutOfRangeException">blockSize is not a positive number, or largeBufferMultiple is not a positive number, or maximumBufferSize is less than blockSize, or maximumSmallPoolFreebytes is negative, or maximumLargePoolFreeBytes is negative.</exception>
        /// <exception cref="ArgumentException">maximumBufferSize is not a multiple/exponential of largeBufferMultiple</exception>
        public RecyclableMemoryStreamManager(int blockSize, int largeBufferMultiple, int maximumBufferSize, bool useExponentialLargeBuffer, long maximumSmallPoolFreeBytes, long maximumLargePoolFreeBytes)
        {
            if (blockSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockSize), blockSize, "blockSize must be a positive number");
            }

            if (largeBufferMultiple <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(largeBufferMultiple),
                                                      "largeBufferMultiple must be a positive number");
            }

            if (maximumBufferSize < blockSize)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumBufferSize),
                                                      "maximumBufferSize must be at least blockSize");
            }

            if (maximumSmallPoolFreeBytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumSmallPoolFreeBytes), "maximumSmallPoolFreeBytes must be non-negative");
            }

            if (maximumLargePoolFreeBytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumLargePoolFreeBytes), "maximumLargePoolFreeBytes must be non-negative");
            }

            this.BlockSize = blockSize;
            this.LargeBufferMultiple = largeBufferMultiple;
            this.MaximumBufferSize = maximumBufferSize;
            this.UseExponentialLargeBuffer = useExponentialLargeBuffer;
            this.MaximumFreeSmallPoolBytes = maximumSmallPoolFreeBytes;
            this.MaximumFreeLargePoolBytes = maximumLargePoolFreeBytes;
            var a = true;
            if (a)
            {
                throw new ArgumentOutOfRangeException(nameof(a));
            }
            if (!this.IsLargeBufferSize(maximumBufferSize))
            {
                throw new ArgumentException(String.Format("maximumBufferSize is not {0} of largeBufferMultiple",
                                                          this.UseExponentialLargeBuffer ? "an exponential" : "a multiple"),
                                            nameof(maximumBufferSize));
            }

            var numLargePools = useExponentialLargeBuffer
                                    ? ((int)Math.Log(maximumBufferSize / largeBufferMultiple, 2) + 1)
                                    : (maximumBufferSize / largeBufferMultiple);

            this.largePools = new ConcurrentStack<byte[]>[numLargePools];

            for (var i = 0; i < this.largePools.Length; ++i)
            {
                this.largePools[i] = new ConcurrentStack<byte[]>();
            }

            Events.Writer.MemoryStreamManagerInitialized(blockSize, largeBufferMultiple, maximumBufferSize);
        }



        /// <summary>
        /// The size of each block. It must be set at creation and cannot be changed.
        /// </summary>
        public int BlockSize { get; }

        /// <summary>
        /// All buffers are multiples/exponentials of this number. It must be set at creation and cannot be changed.
        /// </summary>
        public int LargeBufferMultiple { get; }

        /// <summary>
        /// Use multiple large buffer allocation strategy. It must be set at creation and cannot be changed.
        /// </summary>
        public bool UseMultipleLargeBuffer => !this.UseExponentialLargeBuffer;

        /// <summary>
        /// Use exponential large buffer allocation strategy. It must be set at creation and cannot be changed.
        /// </summary>
        public bool UseExponentialLargeBuffer { get; }

        /// <summary>
        /// Gets the maximum buffer size.
        /// </summary>
        /// <remarks>Any buffer that is returned to the pool that is larger than this will be
        /// discarded and garbage collected.</remarks>
        public int MaximumBufferSize { get; }

        /// <summary>
        /// Number of bytes in small pool not currently in use
        /// </summary>
        public long SmallPoolFreeSize => this.smallPoolFreeSize;

        /// <summary>
        /// Number of bytes currently in use by stream from the small pool
        /// </summary>
        public long SmallPoolInUseSize => this.smallPoolInUseSize;

        /// <summary>
        /// Number of bytes in large pool not currently in use
        /// </summary>
        public long LargePoolFreeSize
        {
            get
            {
                long sum = 0;
                foreach (long freeSize in this.largeBufferFreeSize)
                {
                    sum += freeSize;
                }

                return sum;
            }
        }

        /// <summary>
        /// Number of bytes currently in use by streams from the large pool
        /// </summary>
        public long LargePoolInUseSize
        {
            get
            {
                long sum = 0;
                foreach (long inUseSize in this.largeBufferInUseSize)
                {
                    sum += inUseSize;
                }

                return sum;
            }
        }

        /// <summary>
        /// How many blocks are in the small pool
        /// </summary>
        public long SmallBlocksFree => this.smallPool.Count;

        /// <summary>
        /// How many buffers are in the large pool
        /// </summary>
        public long LargeBuffersFree
        {
            get
            {
                long free = 0;
                foreach (var pool in this.largePools)
                {
                    free += pool.Count;
                }
                return free;
            }
        }

        /// <summary>
        /// How many bytes of small free blocks to allow before we start dropping
        /// those returned to us.
        /// </summary>
        /// <remarks>The default value is 0, meaning the pool is unbounded.</remarks>
        public long MaximumFreeSmallPoolBytes { get; set; }

        /// <summary>
        /// How many bytes of large free buffers to allow before we start dropping
        /// those returned to us.
        /// </summary>
        /// <remarks>The default value is 0, meaning the pool is unbounded.</remarks>
        public long MaximumFreeLargePoolBytes { get; set; }

        /// <summary>
        /// Maximum stream capacity in bytes. Attempts to set a larger capacity will
        /// result in an exception.
        /// </summary>
        /// <remarks>A value of 0 indicates no limit.</remarks>
        public long MaximumStreamCapacity { get; set; }

        /// <summary>
        /// Whether to save callstacks for stream allocations. This can help in debugging.
        /// It should NEVER be turned on generally in production.
        /// </summary>
        public bool GenerateCallStacks { get; set; }

        /// <summary>
        /// Whether dirty buffers can be immediately returned to the buffer pool.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <see cref="RecyclableMemoryStream.GetBuffer"/> is called on a stream and creates a single large buffer, if this setting is enabled, the other blocks will be returned
        /// to the buffer pool immediately.
        /// </para>
        /// <para>
        /// Note when enabling this setting that the user is responsible for ensuring that any buffer previously
        /// retrieved from a stream which is subsequently modified is not used after modification (as it may no longer
        /// be valid).
        /// </para>
        /// </remarks>
        public bool AggressiveBufferReturn { get; set; }

        /// <summary>
        /// Causes an exception to be thrown if <see cref="RecyclableMemoryStream.ToArray"/> is ever called.
        /// </summary>
        /// <remarks>Calling <see cref="RecyclableMemoryStream.ToArray"/> defeats the purpose of a pooled buffer. Use this property to discover code that is calling <see cref="RecyclableMemoryStream.ToArray"/>. If this is 
        /// set and <see cref="RecyclableMemoryStream.ToArray"/> is called, a <c>NotSupportedException</c> will be thrown.</remarks>
        public bool ThrowExceptionOnToArray { get; set; }

        /// <summary>
        /// Returns a buffer of arbitrary size from the large buffer pool. This buffer
        /// will be at least the requiredSize and always be a multiple/exponential of largeBufferMultiple.
        /// </summary>
        /// <param name="requiredSize">The minimum length of the buffer</param>
        /// <param name="id">Unique ID for the stream</param>
        /// <param name="tag">The tag of the stream returning this buffer, for logging if necessary.</param>
        /// <returns>A buffer of at least the required size.</returns>
        /// <exception cref="System.OutOfMemoryException">Requested array size is larger than the maximum allowed.</exception>
        internal byte[] GetLargeBuffer(long requiredSize, Guid id, string tag)
        {
            if (requiredSize > MaxArrayLength)
            {
                throw new OutOfMemoryException($"Requested size exceeds maximum array length of {MaxArrayLength}");
            }

            requiredSize = this.RoundToLargeBufferSize(requiredSize);

            var poolIndex = this.GetPoolIndex(requiredSize);

            bool createdNew = false;
            bool pooled = true;
            string callStack = null;

            byte[] buffer;
            if (poolIndex < this.largePools.Length)
            {
                if (!this.largePools[poolIndex].TryPop(out buffer))
                {
                    buffer = new byte[requiredSize];
                    createdNew = true;
                }
                else
                {
                    Interlocked.Add(ref this.largeBufferFreeSize[poolIndex], -buffer.Length);
                }
            }
            else
            {
                // Buffer is too large to pool. They get a new buffer.

                // We still want to track the size, though, and we've reserved a slot
                // in the end of the inuse array for nonpooled bytes in use.
                poolIndex = this.largeBufferInUseSize.Length - 1;

                // We still want to round up to reduce heap fragmentation.
                buffer = AllocateArray(requiredSize);
                if (this.GenerateCallStacks)
                {
                    // Grab the stack -- we want to know who requires such large buffers
                    callStack = Environment.StackTrace;
                }
                createdNew = true;
                pooled = false;
            }

            Interlocked.Add(ref this.largeBufferInUseSize[poolIndex], buffer.Length);
            if (createdNew)
            {
                ReportLargeBufferCreated(id, tag, requiredSize, pooled: pooled, callStack);
            }

            return buffer;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static byte[] AllocateArray(long requiredSize) =>
#if NET5_0_OR_GREATER
                GC.AllocateUninitializedArray<byte>((int)requiredSize);
#else
                new byte[requiredSize];
#endif
        }

        private long RoundToLargeBufferSize(long requiredSize)
        {
            if (this.UseExponentialLargeBuffer)
            {
                long pow = 1;
                while (this.LargeBufferMultiple * pow < requiredSize)
                {
                    pow <<= 1;
                }
                return this.LargeBufferMultiple * pow;
            }
            else
            {
                return ((requiredSize + this.LargeBufferMultiple - 1) / this.LargeBufferMultiple) * this.LargeBufferMultiple;
            }
        }

        private bool IsLargeBufferSize(int value)
        {
            return (value != 0) && (this.UseExponentialLargeBuffer
                                        ? (value == RoundToLargeBufferSize(value))
                                        : (value % this.LargeBufferMultiple) == 0);
        }

        private int GetPoolIndex(long length)
        {
            if (this.UseExponentialLargeBuffer)
            {
                int index = 0;
                while ((this.LargeBufferMultiple << index) < length)
                {
                    ++index;
                }
                return index;
            }
            else
            {
                return (int)(length / this.LargeBufferMultiple - 1);
            }
        }

        internal void ReportLargeBufferCreated(Guid id, string tag, long requiredSize, bool pooled, string callStack)
        {
            if (pooled)
            {
                Events.Writer.MemoryStreamNewLargeBufferCreated(requiredSize, this.LargePoolInUseSize);
            }
            else
            {
                Events.Writer.MemoryStreamNonPooledLargeBufferCreated(id, tag, requiredSize, callStack);
            }
            this.LargeBufferCreated?.Invoke(this, new LargeBufferCreatedEventArgs(id, tag, pooled));
        }


        /// <summary>
        /// Triggered when a new large buffer is created.
        /// </summary>
        public event EventHandler<LargeBufferCreatedEventArgs> LargeBufferCreated;
    }
}