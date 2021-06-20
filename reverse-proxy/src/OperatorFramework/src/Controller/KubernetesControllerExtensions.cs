using k8s;
using k8s.Models;
//using Microsoft.Kubernetes.Controller.Informers;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KubernetesControllerExtensions
    {
        public static IServiceCollection AddKubernetesControllerRuntime(this IServiceCollection services)
        {
            services = services.AddKubernetesCore();

            return services;
        }

        //public static IServiceCollection RegisterResourceInformer<TResource>(this IServiceCollection services)
        //    where TResource : class, IKubernetesObject<V1ObjectMeta>, new()
        //{
        //    return services
        //        .RegisterHostedService<>
        //}
    }
}
