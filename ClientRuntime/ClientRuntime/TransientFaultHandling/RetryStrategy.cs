using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRuntime.TransientFaultHandling
{
    public delegate RetryCondition ShouldRetryHandler(int retryCount, Exception lastException);

    public abstract class RetryStrategy
    {
        public static readonly int DefaultClientRetryCount = 10;

        public static readonly TimeSpan DefaultRetryInterval = TimeSpan.FromSeconds(1.0);

        public static readonly bool DefaultFirstFastRetry = true;

        /// <summary>
        /// Gets or sets a value indicating whether the first retry attempt will be made immediately,
        /// whereas subsequent retries will remain subject to the retry interval.
        /// </summary>
        public bool FastFirstRetry { get; set; }

        /// <summary>
        /// Gets the name of the retry strategy.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Returns the corresponding ShouldRetry delegate.
        /// </summary>
        /// <returns>The ShouldRetry delegate.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Getter not appropriate for returning new delegate instance for each call.")]
        public abstract ShouldRetryHandler GetShouldRetryHandler();
    }
}
