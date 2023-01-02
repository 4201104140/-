using Microsoft.Extensions.Primitives;
using System.Collections.Specialized;
using System.Diagnostics;

namespace IdentityServer.Extensions;

public static class IReadableStringCollectionExtensions
{
    [DebuggerStepThrough]
    public static NameValueCollection AsNameValueCollection(this IEnumerable<KeyValuePair<string, StringValues>> collection)
    {
        var nv = new NameValueCollection();

        foreach (var field in collection)
        {
            foreach (var val in field.Value)
            {
                // special check for some Azure product: https://github.com/DuendeSoftware/Support/issues/48
                if (!String.IsNullOrWhiteSpace(val))
                {
                    nv.Add(field.Key, val);
                }
            }
        }

        return nv;
    }
}
