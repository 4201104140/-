// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

using Xunit;

namespace Microsoft.ReverseProxy.Abstractions.Tests
{
    public class ProxyRouteTests
    {
        [Fact]
        public void Constructor_Works()
        {
            new ProxyRoute();
        }

        [Fact]
        public void DeepClone_Works()
        {
            var sut = new ProxyRoute
            {
                RouteId = "route1",
                Match =
                {
                    Methods = new[] { "GET" , "POST" },
                    Hosts = new[] { "example.com" },
                    Path = "/",
                    Headers = new[]
                    {
                        new RouteHeader()
                        {
                            Name = "header1",
                            Values = new[] { "value1", "value2" },
                            Mode = HeaderMatchMode.HeaderPrefix,
                            IsCaseSensitive = true,
                        }
                    },
                },
                Order = 2,
                ClusterId = "cluster1",
                AuthorizationPolicy = "policy1",
                CorsPolicy = "policy2",
                Metadata = new Dictionary<string, string>
                {
                    {"key", "value" },
                },
            };

            var clone = sut.DeepClone();

            Assert.NotSame(sut, clone);
            Assert.Equal(sut.RouteId, clone.RouteId);
        }
    }
}
