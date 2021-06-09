using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.FeatureManagement
{
    /// <summary>
    /// Extensions used to add feature management functionality.
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        public static void AddFeatureManagement(this IServiceCollection services)
        {
            services.AddLogging();

            //
            // Add required services


        }
    }
}
