

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

    public abstract class BaseRecyclableMemoryStreamTests
    {
        protected const int DefaultBlockSize = 16384;
        protected const int DefaultLargeBufferMultiple = 1 << 20;
        protected const int DefaultMaximumBufferSize = 8 * (1 << 20);

        protected const string DefaultTag = "NUnit";
        protected static readonly Guid DefaultId = Guid.NewGuid();

        #region RecyclableMemoryManager Tests
        [Test]
        public virtual void RecyclableMemoryManagerUsingMultipleOrExponentialLargeBuffer()
        {
            var memMgr = this.GetMemoryManager();
            Assert.That(memMgr.UseMultipleLargeBuffer, Is.True);
            Assert.That(memMgr.UseExponentialLargeBuffer, Is.False);
        }

        [Test]
        public void RecyclableMemoryManagerThrowsExceptionOnZeroBlockSize()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecyclableMemoryStreamManager(0, 100, 200, this.UseExponentialLargeBuffer));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecyclableMemoryStreamManager(-1, 100, 200, this.UseExponentialLargeBuffer));
            Assert.DoesNotThrow(() => new RecyclableMemoryStreamManager(1, 100, 200, this.UseExponentialLargeBuffer));
        }

        [Test]
        public void RecyclableMemoryManagerThrowsExceptionOnZeroLargeBufferMultipleSize()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecyclableMemoryStreamManager(100, 0, 200, this.UseExponentialLargeBuffer));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecyclableMemoryStreamManager(100, -1, 200, this.UseExponentialLargeBuffer));
            Assert.DoesNotThrow(() => new RecyclableMemoryStreamManager(100, 100, 200, this.UseExponentialLargeBuffer));
        }

        [Test]
        public void RecyclableMemoryManagerThrowsExceptionOnMaximumBufferSizeLessThanBlockSize()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecyclableMemoryStreamManager(100, 100, 99, this.UseExponentialLargeBuffer));
            Assert.DoesNotThrow(() => new RecyclableMemoryStreamManager(100, 100, 100, this.UseExponentialLargeBuffer));
        }

        [Test]
        public virtual void RecyclableMemoryManagerThrowsExceptionOnMaximumBufferNotMultipleOrExponentialOfLargeBufferMultiple()
        {
            Assert.Throws<ArgumentException>(() => new RecyclableMemoryStreamManager(100, 1024, 2025, this.UseExponentialLargeBuffer));
            Assert.Throws<ArgumentException>(() => new RecyclableMemoryStreamManager(100, 1024, 2023, this.UseExponentialLargeBuffer));
            Assert.DoesNotThrow(() => new RecyclableMemoryStreamManager(100, 1024, 2048, this.UseExponentialLargeBuffer));
        }

        [Test]
        public void RecyclableMemoryManagerThrowsExceptionOnNegativeMaxFreeSizes()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecyclableMemoryStreamManager(1, 100, 200, false, -1, 1000));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecyclableMemoryStreamManager(1, 100, 200, false, 1000, -1));
            Assert.DoesNotThrow(() => new RecyclableMemoryStreamManager(1, 100, 200, false, 1000, 1000));
            Assert.DoesNotThrow(() => new RecyclableMemoryStreamManager(1, 100, 200, false, 0, 0));

        }

        [Test]
        public virtual void GetLargeBufferAlwaysAMultipleOrExponentialOfMegabyteAndAtLeastAsMuchAsRequestedForLargeBuffer()
        {
            const int step = 200000;
            const int start = 1;
            const int end = 16000000;
            var memMgr = this.GetMemoryManager();

            for (var i = start; i <= end; i += step)
            {
                var buffer = memMgr.GetLargeBuffer(i, DefaultId, DefaultTag);
                Assert.That(buffer.Length >= i, Is.True);
                Assert.That((buffer.Length % memMgr.LargeBufferMultiple) == 0, Is.True,
                            "buffer length of {0} is not a multiple of {1}", buffer.Length, memMgr.LargeBufferMultiple);
            }
        }

        #endregion

        #region Test Helpers
        protected RecyclableMemoryStream GetDefaultStream()
        {
            return new RecyclableMemoryStream(this.GetMemoryManager());
        }


        protected virtual RecyclableMemoryStreamManager GetMemoryManager()
        {
            return new RecyclableMemoryStreamManager(DefaultBlockSize, DefaultLargeBufferMultiple,
                                                     DefaultMaximumBufferSize, this.UseExponentialLargeBuffer);
        }

        protected virtual bool UseExponentialLargeBuffer
        {
            get { return false; }
        }

        #endregion
    }

    public sealed class RecyclableMemoryStreamTestsWithPassiveBufferRelease : BaseRecyclableMemoryStreamTests
    {
        
    }

    public sealed class RecyclableMemoryStreamTestsWithAggressiveBufferReleaseUsingExponentialLargeBuffer
    {
        


    }
}