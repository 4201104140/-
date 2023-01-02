using IdentityServer.Validation;

#pragma warning disable 1591

namespace IdentityServer.ResponseHandling;

public class AuthorizeResponse
{
    public ValidatedAuthorizeRequest Request { get; set; }
    public string RedirectUri => Request?.RedirectUri;

    public string State => Request?.State;

    public string IdentityToken { get; set; }
}
