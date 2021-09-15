
using BasicOperator.Models;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Kubernetes.ResourceKinds;
using Microsoft.Kubernetes.ResourceKinds.OpenApi;
using System.Threading.Tasks;

namespace TaiOperator
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            // arrange 
            var services = new ServiceCollection();

            // act
            services.AddKubernetesCore();

            // assert
            var serviceProvider = services.BuildServiceProvider();
            var kube = serviceProvider.GetRequiredService<IKubernetes>();
            var client = kube.AnyResourceKind();
            var a = await client.ListClusterAnyResourceKindWithHttpMessagesAsync<V1Pod>(group: "", version: "v1", plural:"pods");
            return 0;
        }
    }
}
