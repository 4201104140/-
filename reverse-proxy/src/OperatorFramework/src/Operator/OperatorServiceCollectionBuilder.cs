// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using k8s;
using k8s.Models;
using Microsoft.Kubernetes;
using Microsoft.Kubernetes.Operator;
using Microsoft.Kubernetes.Operator.Generators;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class OperatorServiceCollectionBuilder<TOperatorResource>
        where TOperatorResource : class, IKubernetesObject<V1ObjectMeta>, new()
    {
        private IServiceCollection _services;

        /// <summary>
        /// Initializes a new instance of <see cref="OperatorServiceCollectionBuilder{TOperatorResource}"/> class
        /// </summary>
        /// <param name="services"></param>
        public OperatorServiceCollectionBuilder(IServiceCollection services)
        {
            _services = services;
        }

        /// <summary>
        /// Gets or sets the services
        /// </summary>
        public IServiceCollection Services => _services;

        /// <summary>
        /// Withes the related resource.
        /// </summary>
        /// <typeparam name="TRelatedResource">The type of the t related resource.</typeparam>
        /// <returns>OperatorServiceCollection&lt;TResource&gt;.</returns>
        //public OperatorServiceCollectionBuilder<TOperatorResource> WithRelatedResource<TRelatedResource>()
        //    where TRelatedResource : class, IKubernetesObject<V1ObjectMeta>, new()
        //{
        //    _services = _services.Reg
        //}
    }
}
