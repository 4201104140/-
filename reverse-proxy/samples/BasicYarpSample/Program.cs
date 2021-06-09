using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BasicYarpSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var myHostBuilder = Host.CreateDefaultBuilder(args);
            myHostBuilder.ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.UseStartup<Startup>();
            });
            var myHost = myHostBuilder.Build();
            myHost.Run();
        }
    }
}
