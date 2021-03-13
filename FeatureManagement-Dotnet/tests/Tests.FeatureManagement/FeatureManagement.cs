// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
//using Microsoft.FeatureManagement.FeatureFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tests.FeatureManagement
{
    public class FeatureManagement
    {
        private const string OnFeature = "OnTestFeature";
        private const string OffFeature = "OffFeature";
        private const string ConditionalFeature = "ConditionalFeature";
        private const string ContextualFeature = "ContextualFeature";

        [Fact]
        public async Task ReadsConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var services = new ServiceCollection();

            services
                .AddSingleton(config)
                .AddFeatureManagement();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IFeatureManager featureManager = serviceProvider.GetRequiredService<IFeatureManager>();

            Assert.False(await featureManager.IsEnabledAsync(OnFeature));
        }




        [Fact]
        public async Task CustomFeatureDefinitionProvider()
        {
            FeatureDefinition testFeature = new FeatureDefinition
            {
                Name = ConditionalFeature
            };

            var services = new ServiceCollection();

            services.AddSingleton<IFeatureDefinitionProvider>(new InMemoryFeatureDefinitionProvider(new FeatureDefinition[] { testFeature }))
                .AddFeatureManagement();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IFeatureManager featureManager = serviceProvider.GetRequiredService<IFeatureManager>();


        }
    }
}
