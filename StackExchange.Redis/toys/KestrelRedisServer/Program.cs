using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;

namespace KestrelRedisServer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            SchedulingMode
            Console.WriteLine("Hello World!");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(server =>
                {
                    server.UseKestrel(options =>
                    {
                        options.ApplicationSchedulingMode = 
                    })
                })
    }
}
