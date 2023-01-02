using IdentityModel;
using IdentityServer.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace IdentityServer.Services;

/// <summary>
/// Cookie-based session implementation
/// </summary>
/// <seealso cref="IUserSession" />
public class DefaultUserSession : IUserSession
{
    /// <summary>
    /// The HTTP context accessor
    /// </summary>
    protected readonly IHttpContextAccessor HttpContextAccessor;

    /// <summary>
    /// The handlers
    /// </summary>
    protected readonly IAuthenticationHandlerProvider Handlers;

    /// <summary>
    /// The logger
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// Gets the HTTP context.
    /// </summary>
    /// <value>
    /// The HTTP context.
    /// </value>
    protected HttpContext HttpContext => HttpContextAccessor.HttpContext;

    /// <summary>
    /// The principal
    /// </summary>
    protected ClaimsPrincipal Principal;

    /// <summary>
    /// The properties
    /// </summary>
    protected AuthenticationProperties Properties;

    /// <summary>
    /// Authenticates the authentication cookie for the current HTTP request and caches the user and properties results.
    /// </summary>
    protected virtual async Task AuthenticateAsync()
    {
        if (Principal == null || Properties == null)
        {
            var scheme = await HttpContext.GetCookieAuthenticationSchemeAsync();

            var handler = await Handlers.GetHandlerAsync(HttpContext, scheme);
            if (handler == null)
            {
                throw new InvalidOperationException($"No authentication handler is configured to authenticate for the scheme: {scheme}");
            }

            var result = await handler.AuthenticateAsync();
            if (result != null && result.Succeeded)
            {
                Principal = result.Principal;
                Properties = result.Properties;
            }
        }
    }

    public Task AddClientIdAsync(string clientId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> CreateSessionIdAsync(ClaimsPrincipal principal, AuthenticationProperties properties)
    {
        if (principal == null) throw new ArgumentNullException(nameof(principal));
        if (properties == null) throw new ArgumentNullException(nameof(properties));

        var currentSubjectId = (await GetUserAsync())?.GetSubjectId();
        var newSubjectId = principal.GetSubjectId();

        if (properties.GetSessionId() == null)
        {
            var currSid = await GetSessionIdAsync();
            if (newSubjectId == currentSubjectId && currSid != null)
            {
                properties.SetSessionId(currSid);
                var clients = Properties.GetClientList();
                if (clients.Any())
                {
                    properties.SetClientList(clients);
                }
            }
            else
            {
                properties.SetSessionId(CryptoRandom.CreateUniqueId(16, CryptoRandom.OutputFormat.Hex));
            }
        }

        var sid = properties.GetSessionId();

        Principal = principal;
        Properties = properties;

        return sid;
    }

    public virtual async Task<ClaimsPrincipal> GetUserAsync()
    {
        await AuthenticateAsync();
        return Principal;
    }

    public async Task<string> GetSessionIdAsync()
    {
        await AuthenticateAsync();

        return Properties?.GetSessionId();
    }

    public Task EnsureSessionIdCookieAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveSessionIdCookieAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the list of clients the user has signed into during their session.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<IEnumerable<string>> GetClientListAsync()
    {
        await AuthenticateAsync();

        if (Properties != null)
        {
            try
            {
                return Properties.GetClientList();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error decoding client list");
                // clear so we don't keep failing
                Properties.RemoveClientList();
                await UpdateSessionCookie();
            }
        }

        return Enumerable.Empty<string>();
    }

    // client list helpers
    private async Task UpdateSessionCookie()
    {
        await AuthenticateAsync();

        if (Principal == null || Properties == null) throw new InvalidOperationException("User is not currently authenticated");

        var scheme = await HttpContext.GetCookieAuthenticationSchemeAsync();
        await HttpContext.SignInAsync(scheme, Principal, Properties);
    }
}
