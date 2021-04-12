using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.Common
{
    public class UnAuthenticatedHttpAdapter : IUnAuthenticatedHttpAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientAdapter> _logger;



        public UnAuthenticatedHttpAdapter(
            ILogger<HttpClientAdapter> logger,
            IHttpClientFactory httpClientFactory
            )
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string> GetRequest(Uri endpoint)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var response = await _httpClient.SendAsync(message);
            var responseString = await response.Content.ReadAsStringAsync();

            if ((int)response.StatusCode >= 400)
            {
                var errorMessage = JsonConvert.DeserializeObject(responseString);
                throw new HttpStatusException(response.StatusCode, errorMessage.ToString());
            }
            return responseString;
        }
        public async Task<string> PostRequest(Uri endpoint, object payload)
        {
            var message = new HttpRequestMessage();
            message.RequestUri = endpoint;
            message.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            message.Method = HttpMethod.Post;

            var response = await _httpClient.SendAsync(message);
            var responseString = await response.Content.ReadAsStringAsync();

            if ((int)response.StatusCode >= 400)
            {
                var errorMessage = JsonConvert.DeserializeObject(responseString);
                throw new HttpStatusException(response.StatusCode, errorMessage.ToString());
            }
            return responseString;
        }
    }
}
