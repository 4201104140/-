//using IdentityServer.Stores;
//using IdentityServer.Validation;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IdentityServer.Services;

///// <summary>
///// Default implementation of IBackchannelAuthenticationInteractionService.
///// </summary>
//public class DefaultBackchannelAuthenticationInteractionService
//{
//    private readonly IBackChannelAuthenticationRequestStore _requestStore;
//    private readonly IClientStore _clientStore;
//    private readonly IUserSession _session;
//    private readonly IResourceValidator _resourceValidator;
//    private readonly ISystemClock _systemClock;
//    private readonly ILogger<DefaultBackchannelAuthenticationInteractionService> _logger;

//    /// <summary>
//    /// Ctor
//    /// </summary>
//    public DefaultBackchannelAuthenticationInteractionService(
//        IBackChannelAuthenticationRequestStore requestStore,
//        IClientStore clients,
//        IUserSession session,
//        IResourceValidator resourceValidator,
//        ISystemClock systemClock,
//        ILogger<DefaultBackchannelAuthenticationInteractionService> logger
//    )
//    {
//        _requestStore = requestStore;
//        _clientStore = clients;
//        _session = session;
//        _resourceValidator = resourceValidator;
//        _systemClock = systemClock;
//        _logger = logger;
//    }

//    public Task<BackchannelUserLoginRequest>
//}
