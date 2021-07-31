// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Kubernetes.Controller.Informers;
using Microsoft.Kubernetes.Controller.Queues;
using Microsoft.Kubernetes.Operator.Caches;
using Microsoft.Kubernetes.Operator.Reconcilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Kubernetes.Operator
{
    public class OperatorHandler<TResource> : IOperatorHandler<TResource>
        where TResource : class, IKubernetesObject<V1ObjectMeta>, new()
    {
        private static GroupApiVersionKind _names = GroupApiVersionKind.From<TResource>();
        private readonly List<IResourceInformerRegistration> _registrations = new List<IResourceInformerRegistration>();
        private readonly IRateLimitingQueue<NamespacedName> _queue;
        private readonly IOperatorCache<TResource> _cache;
        private readonly IOperatorReconciler<TResource> _reconciler;
        private readonly ILogger<OperatorHandler<TResource>> _logger;
        private bool _disposedValue;
    }
}
