
using k8s;
using k8s.Models;
using Microsoft.Kubernetes;
using System.Collections.Generic;
using System.Collections.Immutable;
//using Yarp.ReverseProxy.Kubernetes.Controller.Services;

namespace Yarp.ReverseProxy.Kubernetes.Controller.Caching
{
    /// <summary>
    /// ICache service interface holds onto least amount of data necessary
    /// for <see cref=""/>
    /// </summary>
    public interface ICache
    {
        void Update(WatchEventType eventType, V1Ingress ingress);
        ImmutableList<string> Update(WatchEventType eventType, V1Endpoints endpoints);
        bool TryGetReconcileData(NamespacedName key);
        void GetKeys(List<NamespacedName> keys);
    }
}
