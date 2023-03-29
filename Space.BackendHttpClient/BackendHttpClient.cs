using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.BackendHttpClient
{
    public class BackendHttpClient
    {
        private readonly HttpClient _http;

        public BackendHttpClient(HttpClient http)
        {
            _http = http;
        }

        public Task Get<T>(string uri)
        {
            return Task.CompletedTask;
        }

        public Task Post<T>(string uri)
        {
            return Task.CompletedTask;
        }
    }
}
