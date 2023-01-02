using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Validation;

internal class AuthorizeRequestValidator : IAuthorizeRequestValidator
{
    private readonly ILogger _logger;

    public Task<AuthorizeRequestValidationResult> ValidateAsync(NameValueCollection parameters, ClaimsPrincipal subject = null)
    {
        using var activity = Tracing.BasicActivitySource.StartActivity("AuthorizeRequestValidator.Validate");

        _logger.LogDebug("Start authorize request protocol validation");

        var request = new ValidatedAuthorizeRequest
        {

        }
    }
}
