namespace IdentityServer.Models;

/// <summary>
/// Extensions for Resource
/// </summary>
public static class ResourceExtensions
{
    /// <summary>
    /// Converts to scope names.
    /// </summary>
    /// <param name="resources">The resources.</param>
    /// <returns></returns>
    public static IEnumerable<string> ToScopeNames(this Resources resources)
    {
        var names = resources.IdentityResources.Select(x => x.Name).ToList();
        names.AddRange(resources.ApiScopes.Select(x => x.Name));
        if (resources.OfflineAccess)
        {
            names.Add(IdentityServerConstants.StandardScopes.OfflineAccess);
        }

        return names;
    }
}
