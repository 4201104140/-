using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orleans.ApplicationParts;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using OrleansDashboard;
using OrleansDashboard.Implementation;
using OrleansDashboard.Implementation.Assets;
using OrleansDashboard.Implementation.Details;
using OrleansDashboard.Metrics;
using OrleansDashboard.Metrics.Details;

// ReSharper disable CheckNamespace

namespace Orleans
{
    public static class ServiceCollectionExtensions
    {
        public static ISiloHostBuilder UseDashboard(this ISiloHostBuilder builder,
            Action<DashboardOptions> configurator = null)
        {
            builder.ConfigureApplicationParts(parts => parts.AddDashboardParts());
            builder.ConfigureServices(services => services.AddDashboard(configurator));
            builder.AddStartupTask<Dashboard>();

            return builder;
        }

        public static ISiloBuilder UseDashboard(this ISiloBuilder builder,
            Action<DashboardOptions> configurator = null)
        {
            builder.ConfigureApplicationParts(parts => parts.AddDashboardParts());
            builder.ConfigureServices(services => services.AddDashboard(configurator));
            builder.AddStartupTask<Dashboard>();

            return builder;
        }

        public static IServiceCollection AddDashboard(this IServiceCollection services,
            Action<DashboardOptions> configurator = null)
        {
            services.Configure(configurator ?? (x => { }));
            services.Configure<TelemetryOptions>(options => options.AddConsumer<DashboardTelemetryConsumer>());
            
            services.AddSingleton<>

            return services;
        }

        private static void AddDashboardParts(this IApplicationPartManager appParts)
        {
            appParts
                .AddFrameworkPart(typeof(Dashboard).Assembly)
                .AddFrameworkPart(typeof(IDashboardGrain).Assembly);
        }

        public static IApplicationBuilder UseOrleansDashboard(this IApplicationBuilder app, DashboardOptions options = null)
        {
            if (string.IsNullOrEmpty(options?.BasePath) || options.BasePath == "/")
            {
                app.UseMiddleware<DashboardMiddleware>();
            }
            else
            {
                // Make sure there is a leading slash
                var basePath = options.BasePath.StartsWith("/") ? options.BasePath : $"/{options.BasePath}";

                app.Map(basePath, a => a.UseMiddleware<DashboardMiddleware>());
            }

            return app;
        }

        internal static IServiceCollection AddServicesForHostedDashboard(this IServiceCollection services, IGrainFactory grainFactory, DashboardOptions options)
        {
            services.AddSingleton(DashboardLogger.Instance);
            services.AddSingleton(Options.Create(options));
            services.AddSingleton<ILoggerProvider>(DashboardLogger.Instance);
            services.AddSingleton(grainFactory);
            services.TryAddSingleton<IAssetProvider, CDNAssetProvider>();

            return services;
        }
    }
}
