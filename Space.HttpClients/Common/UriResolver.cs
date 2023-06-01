using Microsoft.AspNetCore.Http;
using Space.Front.Forms.Basic;

namespace Space.HttpClients.Common
{
    public static class UriResolver
    {
        public static string ResolveForFront(string uri, QueryModelBase form)
        {
            var result = uri + QueryString.Create(form.GetParameters().ToDictionary(u => u.Key, v => v.Value));

            return result;
        }
    }
}
