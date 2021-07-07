using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRuntime.TransientFaultHandling
{
    public class RetryPolicy
    {
        public RetryPolicy(ITransientErrorDetectionStrategy errorDetectionStrategy, RetryStrategy retryStrategy)
        {
            this.ErrorDetectionStrategy = errorDetectionStrategy;

            if (errorDetectionStrategy == null)
            {
                throw new InvalidOperationException("");
            }

            this.RetryStrategy = retryStrategy;
        }

        /// <summary>
        /// An instance of a callback delegate that will be invoked whenever a retry condition is encountered.
        /// </summary>
        public event EventHandler<RetryingEventArgs> Retrying;

        /// <summary>
        /// Get delegate count associated with the event
        /// </summary>
        public int EventCallbackCount
        {
            get
            {
                IEnumerable<Delegate> invokeList = this.Retrying?.GetInvocationList();
                if (invokeList != null)
                {
                    return invokeList.Count<Delegate>();
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the retry strategy.
        /// </summary>
        public RetryStrategy RetryStrategy { get; private set; }

        /// <summary>
        /// Gets the instance of the error detection strategy.
        /// </summary>
        public ITransientErrorDetectionStrategy ErrorDetectionStrategy { get; private set; }

        /// <summary>
        /// Repetitively executes the specified action while it satisfies the current retry policy.
        /// </summary>
        /// <param name="action">A delegate that represents the executable action that doesn't return any results.</param>
        public virtual void ExecuteAction(Action action)
        {
            Guard.ArgumentNotNull(action, "action");

            this.ExecuteAction(() => { action(); return default(object); });
        }

        /// <summary>
        /// Repetitively executes the specified action while it satisfies the current retry policy.
        /// </summary>
        /// <typeparam name="TResult">The type of result expected from the executable action.</typeparam>
        /// <param name="func">A delegate that represents the executable action that returns the result of 
        /// type <typeparamref name="TResult"/>.</param>
        /// <returns>The result from the action.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "0", Justification = "Validated with Guard")]
        public virtual TResult ExecuteAction<TResult>(Func<TResult> func)
        {
            Guard.ArgumentNotNull(func, "func");

            int retryCount = 0;
            TimeSpan delay = TimeSpan.Zero;
            Exception lastError;

            var shouldRetry = this.RetryStrategy.GetShouldRetryHandler();

            for (; ; )
            {
                lastError = null;

                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    lastError = ex;

                    if (!(this.ErrorDetectionStrategy.IsTransient(lastError)))
                    {
                        throw;
                    }

                    RetryCondition condition = shouldRetry(retryCount++, lastError);
                    if (!condition.RetryAllowed)
                    {
                        throw;
                    }
                    delay = condition.DelayBeforeRetry;
                }

                // Perform an extra check in the delay interval. Should prevent from accidentally ending up with the 
                // value of -1 that will block a thread indefinitely. In addition, any other negative numbers will 
                // cause an ArgumentOutOfRangeException fault that will be thrown by Thread.Sleep.
                if (delay.TotalMilliseconds < 0)
                {
                    delay = TimeSpan.Zero;
                }

                this.OnRetrying(retryCount, lastError, delay);

                if (retryCount > 1 || !this.RetryStrategy.FastFirstRetry)
                {
                    Task.Delay(delay).Wait();
                }
            }
        }



        /// <summary>
        /// Notifies the subscribers whenever a retry condition is encountered.
        /// </summary>
        /// <param name="retryCount">The current retry attempt count.</param>
        /// <param name="lastError">The exception that caused the retry conditions to occur.</param>
        /// <param name="delay">The delay that indicates how long the current thread will be suspended before 
        /// the next iteration is invoked.</param>
        protected virtual void OnRetrying(int retryCount, Exception lastError, TimeSpan delay)
        {
            if (this.Retrying != null)
            {
                this.Retrying(this, new RetryingEventArgs(retryCount, delay, lastError));
            }
        }
    }
}
