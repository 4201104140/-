// ---------------------------------------------------------------------
// Copyright (c) 2015 Microsoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// ---------------------------------------------------------------------

namespace Microsoft.IO.UnitTests
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.IO;
    using NUnit.Framework;

    /// <summary>
    /// Full test suite. It is abstract to allow parameters of the memory manager to be modified and tested in different
    /// combinations.
    /// </summary>
    public abstract class BaseRecyclableMemoryStreamTests
    {
        protected const int DefaultBlockSize = 16384;
        protected const int DefaultLargeBufferMultiple = 1 << 20;
        protected const int DefaultMaximumBufferSize = 8 * (1 << 20);
        protected const long DefaultVeryLargeStreamSize = 3L * (1L << 30);
        protected const long VeryLargeMaximumSize = 4L * (1L << 30);
        protected const string DefaultTag = "NUnit";
        protected static readonly Guid DefaultId = Guid.NewGuid();

        private readonly Random random = new Random();

        #region RecyclableMemoryManager Tests
        [Test]
        public virtual void RecyclableMemoryManagerUsingMultipleOrExponentialLargeBuffer()
        {
            var memMgr = this.GetMemoryManager();
            Assert.That(memMgr.UseMultipleLargeBuffer, Is.True);
            Assert.That(memMgr.UseExponentialLargeBuffer, Is.False);
        }


        #endregion

        #region Test Helpers
        protected RecyclableMemoryStream GetDefaultStream()
        {
            return new RecyclableMemoryStream(this.GetMemoryManager());
        }

        protected byte[] GetRandomBuffer(int length)
        {
            var buffer = new byte[length];
            random.NextBytes(buffer);
            return buffer;
        }

        protected virtual RecyclableMemoryStreamManager GetMemoryManager()
        {
            return new RecyclableMemoryStreamManager(DefaultBlockSize, DefaultLargeBufferMultiple,
                                                     DefaultMaximumBufferSize, this.UseExponentialLargeBuffer)
            {
                AggressiveBufferReturn = this.AggressiveBufferRelease,
            };
        }

        private RecyclableMemoryStream GetRandomStream()
        {
            var stream = this.GetDefaultStream();
            var buffer = this.GetRandomBuffer(stream.Capacity * 2);
            stream.Write(buffer, 0, buffer.Length);
            stream.Position = 0;
            return stream;
        }
        #endregion

        protected abstract bool AggressiveBufferRelease { get; }

        protected virtual bool UseExponentialLargeBuffer
        {
            get { return false; }
        }
    }

    [TestFixture]
    public sealed class RecyclableMemoryStreamTestsWithPassiveBufferRelease : BaseRecyclableMemoryStreamTests
    {
        protected override bool AggressiveBufferRelease
        {
            get { return false; }
        }

        [Test]
        public void OldBuffersAreKeptInStreamUntilDispose()
        {
            var stream = this.GetMemoryManager();
            //var memMgr = stream.
        }
    }
}
