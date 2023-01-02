namespace IdentityServer.Models;

/// <summary>
/// Models the data to create a token from a validated request.
/// </summary>
public class TokenCreationRequest
{
    /// <summary>
    /// Called to validate the <see cref="TokenCreationRequest"/> before it is processed.
    /// </summary>
    public void Validate()
    {
    }
}
