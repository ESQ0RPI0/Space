using Microsoft.AspNetCore.Http;
using Space.Client.Forms.Basic;

namespace Space.HttpClients.Common
{
    public static class UriResolver
    {
        public static string ResolveForFront(string uri, QueryModelBase form)
        {
            var result = uri + QueryString.Create(form.GetParameters());

            return result;
        }
    }
}
