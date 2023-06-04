using Space.Client.Forms.Basic;
using Space.HttpClients.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace Space.HttpClients
{
    public class FrontApiClient
    {
        private readonly HttpClient _httpClient;

        public FrontApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResult?> Get<TResult>(string actionName)
        {
            var result = await _httpClient.GetFromJsonAsync<TResult?>(actionName);

            return result;
        }
        public async Task<T?> Get<T>(string actionName, QueryModelBase form)
        {
            var finalUri = UriResolver.ResolveForFront(actionName, form);

            var result = await _httpClient.GetFromJsonAsync<T?>(finalUri);

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

            var requestBody = await _httpClient.PostAsync(actionName, content);

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
