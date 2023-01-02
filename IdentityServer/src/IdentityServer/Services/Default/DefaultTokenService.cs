using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace IdentityServer.Services;

/// <summary>
/// Default token service
/// </summary>
public class DefaultTokenService : ITokenService
{
    /// <summary>
    /// The logger
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// The HTTP context accessor
    /// </summary>
    protected readonly IHttpContextAccessor ContextAccessor;

    /// <summary>
    /// The clock
    /// </summary>
    protected readonly ISystemClock Clock;

    public async Task<Token> CreateIdentityTokenAsync(TokenCreationRequest request)
    {
        Logger.LogTrace("Creating identity token");
        request.Validate();

        var claims = new List<Claim>();

        var token = new Token(OidcConstants.TokenTypes.IdentityToken)
        {

        };

        return token;
    }

    public async Task<Token> CreateAccessTokenAsync(TokenCreationRequest request)
    {
        Logger.LogTrace("Creating access token");
        request.Validate();

        var claims = new List<Claim>();

        var token = new Token(OidcConstants.TokenTypes.AccessToken)
        {

        };

        return token;
    }


    public virtual async Task<string> CreateSecurityTokenAsync(Token token)
    {
        string tokenResult;

        if (token.Type == OidcConstants.TokenTypes.AccessToken)
        {

        }
        else if (token.Type == OidcConstants.TokenTypes.IdentityToken)
        {
            Logger.LogTrace("Creating JWT identity token");

            tokenResult = "";
        }
        else
        {
            throw new InvalidOperationException("Invalid token type.");
        }

        return tokenResult;
    }
}
