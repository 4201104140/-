using Microsoft.AspNetCore.Routing;
using Microsoft.ReverseProxy.Service.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarp.ReverseProxy.Management;

namespace Microsoft.AspNetCore.Builder
{
    public static class ReverseProxyIEndpointRouteBuilderExtensions
    {

        public static ReverseProxyConventionBuilder MapReverseProxy(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapReverseProxy(app =>
            {
                
            });
        }

        public static ReverseProxyConventionBuilder MapReverseProxy(this IEndpointRouteBuilder endpoints, Action<IReverseProxyApplicationBuilder> configureApp)
        {
            if (endpoints is null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }
            if (configureApp is null)
            {
                throw new ArgumentNullException(nameof(configureApp));
            }

            return GetOrCreateDataSource(endpoints).DefaultBuilder;
        }

        private static ProxyConfigManager GetOrCreateDataSource(IEndpointRouteBuilder endpoints)
        {
            var dataSource = endpoints.DataSources.OfType<ProxyConfigManager>().FirstOrDefault();


            return dataSource;
        }
    }
}
