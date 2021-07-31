// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Kubernetes.CustomResources;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KubernetesCustomResourceExtensions
    {
        public static IServiceCollection AddKubernetsCustomResources(this IServiceCollection services)
        {
            if (!services.Any(services => services.ServiceType == typeof()))
        }
    }
}
