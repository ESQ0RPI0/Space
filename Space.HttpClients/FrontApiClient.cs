using Space.Client.Forms.Basic;
using Space.HttpClients.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace Space.HttpClients
{
    public class FrontApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string CLIENT_NAME = "api_backend";

        public FrontApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResult?> Get<TResult>(string actionName)
        {
            using var client = _httpClientFactory.CreateClient(CLIENT_NAME);

            var result = await client.GetFromJsonAsync<TResult?>(actionName);

            return result;
        }
        public async Task<T?> Get<T>(string actionName, QueryModelBase form)
        {
            var finalUri = UriResolver.ResolveForFront(actionName, form);

            using var client = _httpClientFactory.CreateClient(CLIENT_NAME);

            var result = await client.GetFromJsonAsync<T?>(finalUri);

            return result;
        }
        /// <summary>
        /// Post request
        /// </summary>
        /// <typeparam name="TResult">Type of result expected to return</typeparam>
        /// <typeparam name="TPayload">Type of payload for request</typeparam>
        /// <param name="actionName">Endpoint name</param>
        /// <param name="payload">Payload for request</param>
        /// <returns></returns>
        public async Task<TResult?> Post<TResult, TPayload>(string actionName, TPayload payload)
        {
            var content = JsonContent.Create(payload);

            using var client = _httpClientFactory.CreateClient();

            var requestBody = await client.PostAsync(actionName, content);

            var result = await requestBody.Content.ReadAsStringAsync();

            if (result != null)
            {
                return JsonSerializer.Deserialize<TResult>(result);
            }
            else
            {
                return JsonSerializer.Deserialize<TResult>(json: string.Empty);
            }
        }
    }
}
