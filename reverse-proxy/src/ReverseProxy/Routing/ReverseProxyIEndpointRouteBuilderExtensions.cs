// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Management;
using Yarp.ReverseProxy.Model;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Routing;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for <see cref="IEndpointRouteBuilder"/>
    /// used to add Reverse Proxy to the ASP .NET Core request pipeline.
    /// </summary>
    public static class ReverseProxyIEndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Adds Reverse Proxy routes to the route table using the default processing pipeline.
        /// </summary>
        public static ReverseProxyConventionBuilder MapReverseProxy(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapReverseProxy(app =>
            {
                app.useSe
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
