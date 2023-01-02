using System.Security.Claims;

namespace IdentityServer.Models;

/// <summary>
/// Models the data to create a refresh token from a validated request.
/// </summary>
public class RefreshTokenCreationRequest
{
    /// <summary>
    /// The client.
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// The subject.
    /// </summary>
    public ClaimsPrincipal Subject { get; set; }

    /// <summary>
    /// The description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The scopes.
    /// </summary>
    public IEnumerable<string> AuthorizedScopes { get; set; }

    /// <summary>
    /// The access token.
    /// </summary>
    public Token AccessToken { get; set; }

    /// <summary>
    /// Called to validate the <see cref="RefreshTokenCreationRequest"/> before it is processed.
    /// </summary>
    public void Validate()
    {
        //if (ValidatedResources == null) throw new ArgumentNullException(nameof(ValidatedResources));
        //if (ValidatedRequest == null) throw new ArgumentNullException(nameof(ValidatedRequest));
    }
}
