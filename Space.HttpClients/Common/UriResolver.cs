using Space.Client.Forms.Basic;
using System.Web;

namespace Space.HttpClients.Common
{
    public static class UriResolver
    {
        public static string ResolveForFront(string uri, QueryModelBase form)
        {
            var result = HttpUtility.ParseQueryString(string.Empty);

            foreach (var param in form.GetParameters())
            {
                result[param.Key] = param.Value;
            }

            if(result.Count == 0)
                return uri;

            return $"{uri}?{result.ToString()}";
        }
    }
}
