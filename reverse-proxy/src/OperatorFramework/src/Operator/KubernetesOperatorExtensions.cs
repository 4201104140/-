// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using k8s;
using k8s.Models;
using Microsoft.Extensions.Options;
using Microsoft.Kubernetes.Operator;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KubernetesOperatorExtensions
    {
        public static IServiceCollection AddKubernetesOperatorRuntime(this IServiceCollection services)
        {
            services = services.AddKubernetesControllerRuntime();

            if (services.Any(services => services.ServiceType == typeof(IOpera)))
        }

        public static IServiceCollection RegisterOperatorResourceInformer<TOperatorResource, TRelatedResource>(this IServiceCollection services)
            where TRelatedResource : class, IKubernetesObject<V1ObjectMeta>, new()
        {
            return services
                .RegisterResourceInformer<TRelatedResource>();
                //.AddTransient<IConfigureOptions<Opera>>
        }

        public static OperatorServiceCollectionBuilder<TResource> AddOperator<TResource>(this IServiceCollection services)
            where TResource : class, IKubernetesObject<V1ObjectMeta>, new()
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services = services
                .AddKue
        }
    }
}
