//using IdentityServer.Extensions;
//using IdentityServer.Hosting;
//using IdentityServer.Models;
//using IdentityServer.Services;
//using Microsoft.Extensions.Logging;
//using System.Collections.Specialized;
//using System.Security.Claims;

//namespace IdentityServer.Endpoints;

//internal abstract class AuthorizeEndpointBase
//{
//    private readonly IAuthorizeRequestValidator _validator;

//    protected ILogger Logger { get; private set; }

//    protected IUserSession UserSession { get; private set; }

//    internal async Task<IEndpointResult> ProcessAuthorizeRequestAsync(NameValueCollection parameters, ClaimsPrincipal user, bool checkConsentResponse = false)
//    {
//        if (user != null)
//        {
//            Logger.LogDebug("User in authorize request: {subjectId}", user.GetSubjectId());
//        }
//        else
//        {
//            Logger.LogDebug("No user present in authorize request");
//        }

//        if (checkConsentResponse)
//        {

//        }

//        var result = 

//        string consentRequestId = null;

//        try
//        {
//            Message<ConsentResponse> consent = null;

//            var request = res
//        }
//    }
//}
