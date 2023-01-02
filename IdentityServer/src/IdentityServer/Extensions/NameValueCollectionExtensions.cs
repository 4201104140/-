using System.Collections.Specialized;

namespace IdentityServer.Extensions;

internal static class NameValueCollectionExtensions
{
    public static IDictionary<string, string[]> ToFullDictionary(this NameValueCollection source)
    {
        return source.AllKeys.ToDictionary(k => k, k => source.GetValues(k));
    }

    public static NameValueCollection FromFullDictionary(this IDictionary<string, string[]> source)
    {
        var nvc = new NameValueCollection();

        foreach ((string key, string[] strings) in source)
        {
            foreach (var value in strings)
            {
                nvc.Add(key, value);
            }
        }

        return nvc;
    }
}
