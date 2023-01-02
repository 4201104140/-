using IdentityServer.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Configuration;

internal class ConfigureInternalCookieOptions : IConfigureNamedOptions<CookieAuthenticationOptions>
{
    private readonly IdentityServerOptions _idsrv;

    public ConfigureInternalCookieOptions(IdentityServerOptions idsrv)
    {
        _idsrv = idsrv;
    }

    public void Configure(CookieAuthenticationOptions options)
    {
    }

    public void Configure(string name, CookieAuthenticationOptions options)
    {
        if (name == IdentityServerConstants.DefaultCookieAuthenticationScheme)
        {
            options.SlidingExpiration = _idsrv.Authentication.CookieSlidingExpiration;
            options.ExpireTimeSpan = _idsrv.Authentication.CookieLifetime;
            options.Cookie.Name = IdentityServerConstants.DefaultCookieAuthenticationScheme;
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = _idsrv.Authentication.CookieSameSiteMode;

            options.LoginPath = ExtractLocalUrl(_idsrv.UserInteraction.LoginUrl);
            options.LogoutPath = ExtractLocalUrl(_idsrv.UserInteraction.LogoutUrl);
            if (_idsrv.UserInteraction.LoginReturnUrlParameter != null)
            {
                options.ReturnUrlParameter = _idsrv.UserInteraction.LoginReturnUrlParameter;
            }
        }

        if (name == IdentityServerConstants.ExternalCookieAuthenticationScheme)
        {
            options.Cookie.Name = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            options.Cookie.IsEssential = true;
            // https://github.com/IdentityServer/IdentityServer4/issues/2595
            // need to set None because iOS 12 safari considers the POST back to the client from the 
            // IdP as not safe, so cookies issued from response (with lax) then should not be honored.
            // so we need to make those cookies issued without same-site, thus the browser will
            // hold onto them and send on the next redirect to the callback page.
            // see: https://brockallen.com/2019/01/11/same-site-cookies-asp-net-core-and-external-authentication-providers/
            options.Cookie.SameSite = _idsrv.Authentication.CookieSameSiteMode;
        }
    }

    private static string ExtractLocalUrl(string url)
    {
        if (url.IsLocalUrl())
        {
            if (url.StartsWith("~/"))
            {
                url = url.Substring(1);
            }

            return url;
        }

        return null;
    }
}


