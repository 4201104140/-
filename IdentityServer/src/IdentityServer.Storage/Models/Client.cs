namespace IdentityServer.Models;

public class Client
{
    public bool Enabled { get; set; } = true;

    public string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the protocol type.
    /// </summary>
    /// <value>
    /// The protocol type.
    /// </value>
    public string ProtocolType { get; set; } = IdentityServerConstants.ProtocolTypes.OpenIdConnect;

    /// <summary>
    /// Client display name (used for logging and consent screen)
    /// </summary>
    public string ClientName { get; set; }

    /// <summary>
    /// Lifetime of authorization code in seconds (defaults to 300 seconds / 5 minutes)
    /// </summary>
    public int AuthorizationCodeLifetime { get; set; } = 300;
}
