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
using Microsoft.FeatureManagement.FeatureFilters;
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
                .AddFeatureManagement()
                .AddFeatureFilter<TestFilter>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IFeatureManager featureManager = serviceProvider.GetRequiredService<IFeatureManager>();

            Assert.True(await featureManager.IsEnabledAsync(OnFeature));

            Assert.False(await featureManager.IsEnabledAsync(OffFeature));

            IEnumerable<IFeatureFilterMetadata> featureFilters = serviceProvider.GetRequiredService<IEnumerable<IFeatureFilterMetadata>>();

            //
            // Sync filter
            TestFilter testFeatureFilter = (TestFilter)featureFilters.First(f => f is TestFilter);

            bool called = false;

            testFeatureFilter.Callback = (evaluationContext) =>
            {
                called = true;

                Assert.Equal("V1", evaluationContext.Parameters["P1"]);

                Assert.Equal(ConditionalFeature, evaluationContext.FeatureName);

                return true;
            };

            await featureManager.IsEnabledAsync(ConditionalFeature);

            Assert.True(called);
        }
    }
}
