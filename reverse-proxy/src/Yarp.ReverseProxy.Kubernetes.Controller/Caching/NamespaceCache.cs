using k8s;
using k8s.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Yarp.ReverseProxy.Kubernetes.Controller.Caching
{
    public class NamespaceCache
    {
        private readonly object _sync = new object();
        private readonly Dictionary<string, ImmutableList<string>> _ingressToServiceNames = new Dictionary<string, ImmutableList<string>>();
        private readonly Dictionary<string, ImmutableList<string>> _serviceToIngressNames = new Dictionary<string, ImmutableList<string>>();
        private readonly Dictionary<string, IngressData> _ingressData = new Dictionary<string, IngressData>();
        private readonly Dictionary<string, Endpoints> _endpointsData = new Dictionary<string, Endpoints>();

        public void Update(WatchEventType eventType, V1Ingress ingress)
        {
            if (ingress is null)
            {
                throw new ArgumentNullException(nameof(ingress));
            }

            var serviceNames = ImmutableList<string>.Empty;

            if (eventType == WatchEventType.Added || eventType == WatchEventType.Modified)
            {
                // If the ingress exists, list out the related services
                var spec = ingress.Spec;
                var defaultBackend = spec?.DefaultBackend;
                var defaultService = defaultBackend?.Service;
                if (!string.IsNullOrEmpty(defaultService?.Name))
                {
                    serviceNames = serviceNames.Add(defaultService.Name);
                }

                foreach (var rule in spec.Rules ?? Enumerable.Empty<V1IngressRule>())
                {
                    var http = rule.Http;
                    foreach (var path in http.Paths ?? Enumerable.Empty<V1HTTPIngressPath>())
                    {
                        var backend = path.Backend;
                        var service = backend.Service;

                        if (!serviceNames.Contains(service.Name))
                        {
                            serviceNames = serviceNames.Add(service.Name);
                        }
                    }
                }
            }

            string ingressName = ingress.Name();
            lock (_sync)
            {

            }
        }
    }
}
