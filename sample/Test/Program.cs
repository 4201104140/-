using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddDebug();
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var test = serviceProvider.GetRequiredService<ILogger<Program>>();

            test.Log<string>(LogLevel.Debug, new EventId(), null, null, (s, e) => "");

        }
    }
}
