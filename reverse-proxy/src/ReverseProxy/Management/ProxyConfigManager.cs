using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yarp.ReverseProxy.Management
{
    internal sealed class ProxyConfigManager : EndpointDataSource, IDisposable
    {
        private readonly object _syncRoot = new object();

        private readonly ConcurrentDictionary<string, RouteState>

        private readonly List<Action<EndpointBuilder>> _conventions;

        private IDisposable? _changeSubscription;

        private List<Endpoint>? _endpoints;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private IChangeToken _changeToken;

        public ProxyConfigManager(
            )
        {

            _conventions = new List<Action<EndpointBuilder>>();
            DefaultBuilder = new ReverseProxyConventionBuilder(_conventions);

            _changeToken = new CancellationChangeToken(_cancellationTokenSource.Token);
        }

        public ReverseProxyConventionBuilder DefaultBuilder { get; }

        public override IReadOnlyList<Endpoint> Endpoints
        {
            get
            {
                // The Endpoints needs to be lazy the first time to give a chance to ReverseProxyConventionBuilder to add its conventions.
                // Endpoints are accessed by routing on the first request.
                if (_endpoints == null)
                {
                    lock (_syncRoot)
                    {
                        if (_endpoints == null)
                        {

                        }
                    }
                }
                return _endpoints;
            }
        }

        [MemberNotNull(nameof(_endpoints)]
        private void CreateEndpoints()
        {
            var endpoints = new List<Endpoint>();

            foreach ()
        }

        public override IChangeToken GetChangeToken() => Volatile.Read(ref _changeToken);

        public void Dispose()
        {
            _changeSubscription?.Dispose();
        }

        
    }
}
