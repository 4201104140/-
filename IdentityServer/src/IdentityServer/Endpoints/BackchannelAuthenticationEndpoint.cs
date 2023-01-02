//using IdentityModel;
//using IdentityServer.Configuration;
//using IdentityServer.Endpoints.Results;
//using IdentityServer.Events;
//using IdentityServer.Extensions;
//using IdentityServer.Hosting;
//using IdentityServer.ResponseHandling;
//using IdentityServer.Services;
//using IdentityServer.Validation;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IdentityServer.Endpoints;

//internal class BackchannelAuthenticationEndpoint : IEndpointHandler
//{
//    private readonly IClientSecretValidator _clientValidator;
//    private readonly IBackchannelAuthenticationRequestValidator _requestValidator;
//    private readonly IBackchannelAuthenticationResponseGenerator _responseGenerator;
//    private readonly IEventService _events;
//    private readonly ILogger<BackchannelAuthenticationEndpoint> _logger;
//    private readonly IdentityServerOptions _options;

//    public BackchannelAuthenticationEndpoint(
//        IClientSecretValidator clientValidator,
//        IBackchannelAuthenticationRequestValidator requestValidator,
//        IBackchannelAuthenticationResponseGenerator responseGenerator,
//        IEventService events,
//        ILogger<BackchannelAuthenticationEndpoint> logger,
//        IdentityServerOptions options)
//    {
//        _clientValidator = clientValidator;
//        _requestValidator = requestValidator;
//        _responseGenerator = responseGenerator;
//        _events = events;
//        _logger = logger;
//        _options = options;
//    }

//    private async Task<IEndpointResult> ProcessAuthenticationRequestAsync(HttpContext context)
//    {
//        _logger.LogDebug("Start backchannel authentication request.");

//        // validate client
//        var clientResult = await _clientValidator.ValidateAsync(context);
//        if (clientResult.IsError)
//        {
//            return Error(clientResult.Error ?? OidcConstants.BackchannelAuthenticationRequestErrors.InvalidClient);
//        }

//        // validate request
//        var form = (await context.Request.ReadFormAsync()).AsNameValueCollection();
//        _logger.LogTrace("Calling into backchannel authentication request validator: {type}", _requestValidator.GetType().FullName);
//        var requestResult = await _requestValidator.ValidateRequestAsync(form, clientResult);

//        if (requestResult.IsError)
//        {
//            await _events.RaiseAsync(new BackchannelAuthenticationFailureEvent(requestResult));
//            return Error(requestResult.Error, requestResult.ErrorDescription);
//        }

//        // create response
//        _logger.LogTrace("Calling into backchannel authentication request response generator: {type}", _responseGenerator.GetType().FullName);
//        var response = await _responseGenerator.ProcessAsync(requestResult);

//        await _events.RaiseAsync(new BackchannelAuthenticationSuccessEvent(requestResult));
//        LogResponse(response, requestResult);

//        // return result
//        _logger.LogDebug("Backchannel authentication request success.");
//        return new BackchannelAuthenticationResult(response);
//    }

//    BackchannelAuthenticationResult Error(string error, string errorDescription = null)
//    {
//        return new BackchannelAuthenticationResult(new BackchannelAuthenticationResponse(error, errorDescription));
//    }
//}
