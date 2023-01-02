//using IdentityServer.Models;
//using IdentityServer.Validation;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IdentityServer.ResponseHandling;

//public class AuthorizeResponseGenerator : IAuthorizeResponseGenerator
//{
//    /// <summary>
//    /// The logger
//    /// </summary>
//    protected readonly ILogger Logger;

//    /// <summary>
//    /// The clock
//    /// </summary>
//    protected readonly ISystemClock Clock;

//    public virtual async Task<AuthorizeResponse> CreateResponseAsync(ValidatedAuthorizeRequest request)
//    {
//        using var activity = Tracing.BasicActivitySource.StartActivity("AuthorizeResponseGenerator.CreateResponse");

//        if (request.GrantType == GrantType.AuthorizationCode)
//        {

//        }
//        if (request.GrantType == GrantType.Implicit)
//        {

//        }
//        if (request.GrantType == GrantType.Hybrid)
//        {

//        }

//        throw new InvalidOperationException("invalid grant type: " + request.GrantType);
//    }

//    protected virtual async Task<AuthorizationCode> CreateCodeAsync(ValidatedAuthorizeRequest request)
//    {
//        string stateHash = null;

//        var code = new AuthorizationCode
//        {
//            CreationTime = Clock.UtcNow.UtcDateTime,
//            ClientId = request.Client.ClientId,
//            Lifetime = request.Client.AuthorizationCodeLifetime,

//        };

//        return code;
//    }

//    /// <summary>
//    /// Creates the response for a hybrid flow request
//    /// </summary>
//    /// <param name="request"></param>
//    /// <returns></returns>
//    protected virtual async Task<AuthorizeResponse> CreateHybridFlowResponseAsync(ValidatedAuthorizeRequest request)
//    {
//        Logger.LogDebug("Creating Hybird Flow response.");

//        var code = await CreateCodeAsync(request);
//        var id = await 
//    }

//    /// <summary>
//    /// Creates the response for a code flow request
//    /// </summary>
//    /// <param name="request"></param>
//    /// <returns></returns>
//    protected virtual async Task<AuthorizeResponse> CreateCodeFlowResponseAsync(ValidatedAuthorizeRequest request)
//    {
//        Logger.LogDebug("Creating Authorization Code Flow response.");

//        var code = await CreateCodeAsync(request);
//        var id = await
//    }
//}
