using Microsoft.AspNetCore.WebUtilities;
using Space.Front.Forms.Basic;

namespace Space.HttpClients.Common
{
    public static class UriResolver
    {
        public static string ResolveForFront(string uri, QueryModelBase form)
        {
            var result = QueryHelpers.AddQueryString(uri, form.GetParameters().ToDictionary(u => u.Key, v => v.Value));

            return result;
        }
    }
}
