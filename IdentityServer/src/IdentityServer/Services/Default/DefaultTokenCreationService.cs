using IdentityServer.Configuration;
using IdentityServer.Extensions;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IdentityServer.Services;

/// <summary>
/// Default token creation service
/// </summary>
public class DefaultTokenCreationService : ITokenCreationService
{
    /// <summary>
    /// The key service
    /// </summary>
    protected readonly IKeyMaterialService Keys;

    /// <summary>
    /// The logger
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    ///  The clock
    /// </summary>
    protected readonly ISystemClock Clock;

    /// <summary>
    /// The options
    /// </summary>
    protected readonly IdentityServerOptions Options;

    public virtual async Task<string> CreateTokenAsync(Token token)
    {
        var payload = await CreatePayloadAsync(token);
        var headerElements = await CreateHeaderElementsAsync(token);

        return await CreateJwtAsync(token, payload, headerElements);
    }

    /// <summary>
    /// Creates the JWT payload
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    protected virtual Task<string> CreatePayloadAsync(Token token)
    {
        var payload = token.CreateJwtPayloadDictionary(Options, Clock, Logger);
        return Task.FromResult(JsonSerializer.Serialize(payload));
    }

    /// <summary>
    /// Creates additional JWT header elements
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    protected virtual Task<Dictionary<string, object>> CreateHeaderElementsAsync(Token token)
    {
        var additionalHeaderElements = new Dictionary<string, object>();

        if (token.Type == IdentityServerConstants.TokenTypes.AccessToken)
        {
            if (Options.AccessTokenJwtType.IsPresent())
            {
                additionalHeaderElements.Add("typ", Options.AccessTokenJwtType);
            }
        }

        return Task.FromResult(additionalHeaderElements);
    }

    protected virtual async Task<string> CreateJwtAsync(Token token, string payload,
        Dictionary<string, object> headerElements)
    {

        var handler = new JsonWebTokenHandler { SetDefaultTimesOnTokenCreation = false };
        return handler.CreateToken(payload, )
    }
}
