using System.Security.Claims;

namespace IdentityServer.Models;

/// <summary>
/// Models a refresh token.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Gets or sets the creation time.
    /// </summary>
    /// <value>
    /// The creation time.
    /// </value>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Gets or sets the life time.
    /// </summary>
    /// <value>
    /// The life time.
    /// </value>
    public int Lifetime { get; set; }

    /// <summary>
    /// Gets or sets the consumed time.
    /// </summary>
    /// <value>
    /// The consumed time.
    /// </value>
    public DateTime? ConsumedTime { get; set; }

    /// <summary>
    /// Gets or sets the resource indicator specific access token.
    /// </summary>
    /// <value>
    /// The access token.
    /// </value>
    public Dictionary<string, Token> AccessTokens { get; set; } = new Dictionary<string, Token>();

    /// <summary>
    /// Sets the access token based on the resource indicator.
    /// </summary>
    /// <param name="resourceIndicator"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public void SetAccessToken(Token token, string resourceIndicator = null)
    {
        AccessTokens[resourceIndicator ?? String.Empty] = token;
    }

    /// <summary>
    /// Gets or sets the original subject that requested the token.
    /// </summary>
    /// <value>
    /// The subject.
    /// </value>
    public ClaimsPrincipal Subject { get; set; }

    /// <summary>
    /// Gets or sets the version number.
    /// </summary>
    /// <value>
    /// The version.
    /// </value>
    public int Version { get; set; } = 5;

    /// <summary>
    /// Gets the client identifier.
    /// </summary>
    /// <value>
    /// The client identifier.
    /// </value>
    public string ClientId { get; set; }
}
