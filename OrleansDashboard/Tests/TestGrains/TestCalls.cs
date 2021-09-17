using System;
using System.Threading;
using System.Threading.Tasks;
using Orleans;

namespace TestGrains
{
    public static class TestCalls
    {
        public static Task Make(IClusterClient client, CancellationTokenSource tokenSource)
        {
            return Task.Run(async () =>
            {
                var random = new Random();

                while (!tokenSource.IsCancellationRequested)
                {
                    try
                    {
                        var testGrain = client.GetGrain<ITestGrain>(random.Next(500));

                        await Task.Delay(2000);
                    }
                    catch
                    {
                        // Grain might throw exception to test error rate.
                    }
                }
            });
        }
    }
}
