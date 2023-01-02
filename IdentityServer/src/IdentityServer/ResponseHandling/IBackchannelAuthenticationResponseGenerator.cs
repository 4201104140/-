using IdentityServer.Validation;

namespace IdentityServer.ResponseHandling;

/// <summary>
/// Interface the backchannel authentication response generator
/// </summary>
public interface IBackchannelAuthenticationResponseGenerator
{
    /// <summary>
    /// Processes the response.
    /// </summary>
    /// <param name="validationResult">The validation result.</param>
    /// <returns></returns>
    Task<BackchannelAuthenticationResponse> ProcessAsync(BackchannelAuthenticationRequestValidationResult validationResult);
}
