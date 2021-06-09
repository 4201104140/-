using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarp.ReverseProxy.Management
{
    internal sealed class ProxyConfigManager : /*EndpointDataSource,*/ IDisposable
    {

        public ReverseProxyConventionBuilder DefaultBuilder { get; }

        public void Dispose()
        {

        }
    }
}
