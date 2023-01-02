using IdentityServer.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// DI extension methods for adding IdentityServer
/// </summary>
public static class IdentityServerServiceCollectionExtensions
{
    /// <summary>
    /// Creates a builder.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddIdentityServerBuilder(this IServiceCollection services)
    {
        return new IdentityServerBuilder(services);
    }

    /// <summary>
    /// Adds IdentityServer.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddIdentityServer(this IServiceCollection services)
    {
        var builder = services.AddIdentityServerBuilder();

        return builder;
    }

    public static IIdentityServerBuilder AddIdentityServer(this IServiceCollection services)
    {
        var builder = services.AddIdentityServerBuilder();

        builder
            .AddRequiredPlatformServices()
            .AddCookieAuthentication()
            .AddCoreServices()
            .AddDefaultEndpoints()
            .AddPluggableServices()
            .AddKeyManagement()
            .AddDynamicProvidersCore()
            .AddOidcDynamicProvider()
            .AddValidators()
            .AddResponseGenerators()
            .AddDefaultSecretParsers()
            .AddDefaultSecretValidators();

        return builder;
    }

    /// <summary>
    /// Adds IdentityServer.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="setupAction">The setup action.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddIdentityServer(this IServiceCollection services, Action<IdentityServerOptions> setupAction)
    {
        services.Configure(setupAction);
        return services.AddIdentityServer();
    }
}
