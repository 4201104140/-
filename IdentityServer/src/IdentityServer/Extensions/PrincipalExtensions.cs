﻿using IdentityModel;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace IdentityServer.Extensions;

/// <summary>
/// Extension methods for <see cref="System.Security.Principal.IPrincipal"/> and <see cref="System.Security.Principal.IIdentity"/> .
/// </summary>
public static class PrincipalExtensions
{
    /// <summary>
    /// Gets the subject identifier.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static string GetSubjectId(this IPrincipal principal)
    {
        return principal.Identity.GetSubjectId();
    }

    /// <summary>
    /// Gets the subject identifier.
    /// </summary>
    /// <param name="identity">The identity.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">sub claim is missing</exception>
    [DebuggerStepThrough]
    public static string GetSubjectId(this IIdentity identity)
    {
        var id = identity as ClaimsIdentity;
        var claim = id.FindFirst(JwtClaimTypes.Subject);

        if (claim == null) throw new InvalidOperationException("sub claim is missing");
        return claim.Value;
    }
}
