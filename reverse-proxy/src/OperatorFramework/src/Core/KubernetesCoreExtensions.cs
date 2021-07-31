
using k8s;
using Microsoft.Extensions.Options;
using Microsoft.Kubernetes;
using Microsoft.Kubernetes.Client;
using Microsoft.Kubernetes.Resources;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class KubernetsCoreExtensions.
    /// </summary>
    public static class KubernetesCoreExtensions
    {
        /// <summary>
        /// Adds the kubernetes.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddKubernetesCore(this IServiceCollection services)
        {
            if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IKubernetes)))
            {
                services = services.Configure<KubernetesClientOptions>(options =>
                {
                    if (options.Configuration == null)
                    {
                        options.Configuration = KubernetesClientConfiguration.BuildDefaultConfig();
                    }
                });

                services = services.AddSingleton<IKubernetes>(sp =>
                {
                    var options = sp.GetRequiredService<IOptions<KubernetesClientOptions>>().Value;

                    return new k8s.Kubernetes(options.Configuration);
                });
            }

            if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResourceSerializers)))
            {
                services = services.AddTransient<IResourceSerializers, ResourceSerializers>();
            }

            return services;
        }
    }
}

namespace k8s
{
    public static class KubernetesHelpersExtensions
    {
        public static IAnyResourceKind AnyResourceKind(this IKubernetes client)
        {
            return new AnyResourceKind(client);
        }
    }
}
