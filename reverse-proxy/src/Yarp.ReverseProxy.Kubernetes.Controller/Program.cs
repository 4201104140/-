using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;
using System.Threading.Tasks;

namespace Yarp.ReverseProxy.Kubernetes.Controller
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            ServiceClientTracing.IsEnabled = true;

            await Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()
                .StartAsync().ConfigureAwait(false);
        }
    }
}
