namespace Microsoft.IO
{
    using System;

    public sealed partial class RecyclableMemoryStreamManager
    {
        /// <summary>
        /// Arguments for the StreamCreated event
        /// </summary>
        public sealed class StreamCreatedEventArgs : EventArgs
        {
            /// <summary>
            /// Unique ID for the stream
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// Optional Tag for the event
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// Requested stream size
            /// </summary>
            public long RequestedSize { get; }

            /// <summary>
            /// Actual stream size
            /// </summary>
            public long ActualSize { get; }

            /// <summary>
            /// Initializes a StreamCreatedEventArgs struct
            /// </summary>
            /// <param name="guid">Unique ID of the stream</param>
            /// <param name="tag">Tag of the stream</param>
            /// <param name="requestedSize">The requested stream size</param>
            /// <param name="actualSize">The actual stream size</param>
            public StreamCreatedEventArgs(Guid guid, string tag, long requestedSize, long actualSize)
            {
                this.Id = guid;
                this.Tag = tag;
                this.RequestedSize = requestedSize;
                this.ActualSize = actualSize;
            }
        }

        /// <summary>
        /// Arguments for the StreamDisposed event
        /// </summary>
        public sealed class StreamDisposedEventArgs : EventArgs
        {
            /// <summary>
            /// Unique ID for the stream
            /// </summary>
            public Guid Id { get; }
            /// <summary>
            /// Optional Tag for the event
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// Stack where the stream was allocated
            /// </summary>
            public string AllocationStack { get; }

            /// <summary>
            /// Stack where stream was disposed
            /// </summary>
            public string DisposeStack { get; }

            /// <summary>
            /// Initializes a StreamDisposedEventArgs struct
            /// </summary>
            /// <param name="guid">Unique ID of the stream</param>
            /// <param name="tag">Tag of the stream</param>
            /// <param name="allocationStack">Stack of original allocation</param>
            /// <param name="disposeStack">Dispose stack</param>
            public StreamDisposedEventArgs(Guid guid, string tag, string allocationStack, string disposeStack)
            {
                this.Id = guid;
                this.Tag = tag;
                this.AllocationStack = allocationStack;
                this.DisposeStack = disposeStack;
            }
        }


        /// <summary>
        /// Arguments for the LargeBufferCreated events
        /// </summary>
        public sealed class LargeBufferCreatedEventArgs : EventArgs
        {
            /// <summary>
            /// Unique ID for the stream
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// Optional Tag for the event
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// Whether the buffer was satisfied from the pool or not
            /// </summary>
            public bool Pooled { get; }

            /// <summary>
            /// Initializes a LargeBufferCreatedEventArgs struct
            /// </summary>
            /// <param name="guid">Unique ID of the stream</param>
            /// <param name="tag">Tag of the stream</param>
            /// <param name="pooled"></param>
            public LargeBufferCreatedEventArgs(Guid guid, string tag, bool pooled)
            {
                this.Pooled = pooled;
                this.Id = guid;
                this.Tag = tag;
            }
        }
    }
}