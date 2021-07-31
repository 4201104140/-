// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using BasicOperator.Generators;
using BasicOperator.Models;
using k8s.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Kubernetes.ResourceKinds;
using Microsoft.Kubernetes.ResourceKinds.OpenApi;
using System.Threading.Tasks;

namespace BasicOperator
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var hostbuilder = new HostBuilder();

            hostbuilder.ConfigureHostConfiguration(hostConfiguration =>
            {
                hostConfiguration.AddCommandLine(args);
            });

            hostbuilder.ConfigureServices(services =>
            {
                services.AddTransient<IResourceKindProvider, OpenApiResourceKindProvider>();

                services.AddOperator
            });

            await hostbuilder.RunConsoleAsync();
            return 0;
        }
    }
}
