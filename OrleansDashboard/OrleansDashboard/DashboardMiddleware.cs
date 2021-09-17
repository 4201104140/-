using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Orleans;
using OrleansDashboard.Implementation;
using OrleansDashboard.Implementation.Assets;
using OrleansDashboard.Model;

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace OrleansDashboard
{
    public sealed class DashboardMiddleware
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IncludeFields = true,
        };

        static DashboardMiddleware()
        {
            Options.Converters.Add(new TimeSpanConverter());
        }

        private const int REMINDER_PAGE_SIZE = 50;
        private readonly IOptions<DashboardOptions> options;
        private readonly DashboardLogger logger;
        private readonly RequestDelegate next;
        private readonly IAssetProvider assetProvider;
        private readonly IDashboardClient client;

        public DashboardMiddleware(RequestDelegate next,
            IGrainFactory grainFactory,
            IAssetProvider assetProvider,
            IOptions<DashboardOptions> options,
            DashboardLogger logger)
        {
            this.options = options;
            this.logger = logger;
            this.next = next;
            this.assetProvider = assetProvider;
            client = new DashboardClient(grainFactory);
        }


    }
}
