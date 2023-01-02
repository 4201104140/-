namespace IdentityServer.Configuration;

/// <summary>
/// The IdentityServerOptions class is the top level container for all configuration settings of IdentityServer.
/// </summary>
public class IdentityServerOptions
{
    /// <summary>
    /// Specifies whether scopes in JWTs are emitted as array or string
    /// </summary>
    public bool EmitScopesAsSpaceDelimitedStringInJwt { get; set; } = false;

    /// <summary>
    /// Gets or sets the value for the JWT typ header for access tokens.
    /// </summary>
    /// <value>
    /// The JWT typ value.
    /// </value>
    public string AccessTokenJwtType { get; set; } = "at+jwt";

    /// <summary>
    /// Gets or sets the endpoint configuration.
    /// </summary>
    /// <value>
    /// The endpoints configuration.
    /// </value>
    public EndpointsOptions Endpoints { get; set; } = new EndpointsOptions();

    /// <summary>
    /// Gets or sets the authentication options.
    /// </summary>
    /// <value>
    /// The authentication options.
    /// </value>
    public AuthenticationOptions Authentication { get; set; } = new AuthenticationOptions();

    /// <summary>
    /// Gets or sets the Content Security Policy options.
    /// </summary>
    public CspOptions Csp { get; set; } = new CspOptions();
}
