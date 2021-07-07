using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarp.ReverseProxy.Management;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ReverseProxyServiceCollectionExtensions
    {
        /// <summary>
        /// Adds ReverseProxy's services to Dependency Injection.
        /// </summary>
        public static IReverseProxyBuilder AddReverseProxy(this IServiceCollection services)
        {
            var builder = new ReverseProxyBuilder(services);
            builder
                .AddConfigBuilder();

            services.AddDataProtection();
            services.AddCors();
            services.AddRouting();

            return builder;
        }

        /// <summary>
        /// Loads routes and endpoints from config.
        /// </summary>
        public static IReverseProxyBuilder LoadFromConfig(this IReverseProxyBuilder builder, IConfiguration config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            builder.Services.AddSingleton<>

            return builder;
        }

        //public static IReverseProxyBuilder AddTransformFactory<T>(this IReverseProxyBuilder builder) where T : class, 
    }
}
