using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.ReverseProxy.Management
{
    internal static class IReverseProxyBuilderExtensions
    {
        public static IReverseProxyBuilder AddConfigBuilder(this IReverseProxyBuilder builder)
        {
            builder.Services.TryAddSingleton<IConfigValidator, ConfigValidator>();

            return builder;
        }


    }
}
