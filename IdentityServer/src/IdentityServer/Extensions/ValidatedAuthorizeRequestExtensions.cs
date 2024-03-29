﻿namespace IdentityServer.Validation;

public static class ValidatedAuthorizeRequestExtensions
{

    public static string GetPrefixedAcrValue(this ValidatedAuthorizeRequest request, string prefix)
    {
        var value = request.AuthenticationContextReferenceClasses
            .FirstOrDefault(x => x.StartsWith(prefix));

        if (value != null)
        {
            value = value.Substring(prefix.Length);
        }

        return value;
    }

    public static string GetIdP(this ValidatedAuthorizeRequest request)
    {
        return request.GetPrefixedAcrValue(Constants.KnownAcrValues.HomeRealm);
    }

    public static string GetTenant(this ValidatedAuthorizeRequest request)
    {
        return request.GetPrefixedAcrValue(Constants.KnownAcrValues.Tenant);
    }

    public static IEnumerable<string> GetAcrValues(this ValidatedAuthorizeRequest request)
    {
        return request
            .AuthenticationContextReferenceClasses
            .Where(acr => !Constants.KnownAcrValues.All.Any(well_known => acr.StartsWith(well_known)))
            .Distinct()
            .ToArray();
    }
}
