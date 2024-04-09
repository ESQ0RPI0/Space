using Space.Client.Forms.Basic;
using System.Web;

namespace Space.HttpClients.Common
{
    public static class UriResolver
    {
        public static string ResolveForFront(string uri, QueryModelBase form)
        {
            var result = HttpUtility.ParseQueryString(uri);

            foreach (var param in form.GetParameters())
            {
                result[param.Key] = param.Value;
            }

            return result.ToString();
        }
    }
}
