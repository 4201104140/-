using IdentityModel;
using System.Security.Claims;

namespace IdentityServer.Models;

/// <summary>
/// Models a token.
/// </summary>
public class Token
{
	public Token()
	{
	}

    /// <summary>
    /// Initializes a new instance of the <see cref="Token"/> class.
    /// </summary>
    /// <param name="tokenType">Type of the token.</param>
    public Token(string tokenType)
    {
        Type = tokenType;
    }

    public string Confirmation { get; set; }

    /// <summary>
    /// Gets or sets the audiences.
    /// </summary>
    /// <value>
    /// The audiences.
    /// </value>
    public ICollection<string> Audiences { get; set; } = new HashSet<string>();

    /// <summary>
    /// Gets or sets the issuer.
    /// </summary>
    /// <value>
    /// The issuer.
    /// </value>
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the lifetime.
    /// </summary>
    /// <value>
    /// The lifetime.
    /// </value>
    public int Lifetime { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    public string Type { get; set; } = OidcConstants.TokenTypes.AccessToken;

    /// <summary>
    /// Gets or sets the claims.
    /// </summary>
    /// <value>
    /// The claims.
    /// </value>
    public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>(new ClaimComparer());

    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    /// <value>
    /// The version.
    /// </value>
    public int Version { get; set; } = 5;
}
