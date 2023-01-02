using IdentityServer.Configuration;
using IdentityServer.Hosting;
using IdentityServer.Hosting.DynamicProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Pipeline extension methods for adding IdentityServer
/// </summary>
public static class IdentityServerApplicationBuilderExtensions
{
    public static IApplicationBuilder UseIdentityServer(this IApplicationBuilder app, IdentityServerMiddlewareOptions options = null)
    {
        app.Validate();

        app.UseMiddleware<BaseUrlMiddleware>();

        app.UseMiddleware<DynamicSchemeAuthenticationMiddleware>();

        if (options == null) options = new IdentityServerMiddlewareOptions();
        options.AuthenticationMiddleware(app);

        //app.UseMiddleware<>

        return app;
    }

    internal static void Validate(this IApplicationBuilder app)
    {
        var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
        if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));

        var logger = loggerFactory.CreateLogger("Duende.IdentityServer.Startup");
        logger.LogInformation("Starting Duende IdentityServer version {version} ({netversion})", typeof(IdentityServer.Hosting.IdentityServerMiddleware).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion, RuntimeInformation.FrameworkDescription);

        var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

        using (var scope = scopeFactory.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            var options = serviceProvider.GetRequiredService<IdentityServerOptions>();
            var env = serviceProvider.GetRequiredService<IHostEnvironment>();

        }
    }
}
