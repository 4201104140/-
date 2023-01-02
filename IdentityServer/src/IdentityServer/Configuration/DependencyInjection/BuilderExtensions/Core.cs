using IdentityServer;
using IdentityServer.Configuration;
using IdentityServer.Endpoints;
using IdentityServer.Extensions;
using IdentityServer.Hosting;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using static IdentityServer.Constants;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Builder extension methods for registering core services
/// </summary>
public static class IdentityServerBuilderExtensionsCore
{
    public static IIdentityServerBuilder AddRequiredPlatformServices(this IIdentityServerBuilder builder)
    {
        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddOptions();
        builder.Services.AddSingleton(
            resolver => resolver.GetRequiredService<IOptions<IdentityServerOptions>>().Value);
        builder.Services.AddTransient(
            resolver => resolver.GetRequiredService<IOptions<IdentityServerOptions>>().Value.PersistentGrants);
        builder.Services.AddHttpClient();

        return builder;
    }

    public static IIdentityServerBuilder AddCookieAuthentication(this IIdentityServerBuilder builder)
    {
        return builder

    }

    /// <summary>
    /// Adds the default cookie handlers and corresponding configuration
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddDefaultCookieHandlers(this IIdentityServerBuilder builder)
    {
        builder.Services.AddAuthentication(IdentityServerConstants.DefaultCookieAuthenticationScheme)
            .AddCookie(IdentityServerConstants.DefaultCookieAuthenticationScheme)
            .AddCookie(IdentityServerConstants.ExternalCookieAuthenticationScheme);
        builder.Services.AddSingleton<IConfigureOptions<CookieAuthenticationOptions>, ConfigureInternalCookieOptions>();

        return builder;
    }

    public static IIdentityServerBuilder AddDefaultEndpoints(this IIdentityServerBuilder builder)
    {
        builder.Services.AddTransient<IEndpointRouter, EndpointRouter>();

        builder.AddEndpoint<DiscoveryKeyEndpoint>(EndpointNames.Discovery, ProtocolRoutePaths.DiscoveryWebKeys.EnsureLeadingSlash());

        return builder;
    }

    /// <summary>
    /// Adds the endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddEndpoint<T>(this IIdentityServerBuilder builder, string name, PathString path)
        where T : class, IEndpointHandler
    {
        builder.Services.AddTransient<T>();
        builder.Services.AddSingleton(new IdentityServer.Hosting.Endpoint(name, path, typeof(T)));

        return builder;
    }

    public static IIdentityServerBuilder AddCoreServices(this IIdentityServerBuilder builder)
    {
        //builder.Services.AddTransient<IServerUrls, DefaultServerUrls>();

        return builder;
    }

    public static IIdentityServerBuilder AddPluggableServices(this IIdentityServerBuilder builder)
    {
        return builder;
    }

    public static IIdentityServerBuilder AddKeyManagement(this IIdentityServerBuilder builder)
    {
        return builder;
    }

    public static IIdentityServerBuilder AddDynamicProvidersCore(this IIdentityServerBuilder builder)
    {
        return builder;
    }

    public static IIdentityServerBuilder AddValidators(this IIdentityServerBuilder builder)
    {
        return builder;
    }

    public static IIdentityServerBuilder AddResponseGenerators(this IIdentityServerBuilder builder)
    {
        return builder;
    }

    public static IIdentityServerBuilder AddDefaultSecretParsers(this IIdentityServerBuilder builder)
    {
        return builder;
    }

    public static IIdentityServerBuilder AddDefaultSecretValidators(this IIdentityServerBuilder builder)
    {
        return builder;
    }


}
