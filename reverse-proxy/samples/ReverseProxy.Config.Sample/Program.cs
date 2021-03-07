using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Microsoft.ReverseProxy.Sample
{
    /// <summary>
    /// Class that contains the entrypoint for the Reverse Proxy sample app.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entrypoint of the application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
