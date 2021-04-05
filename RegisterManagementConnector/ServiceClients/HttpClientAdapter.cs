using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IHttpClientFactory = System.Net.Http.IHttpClientFactory;
using Microsoft.Extensions.Logging;
using Polly;
using OpenReferrals.RegisterManagementConnector.Configuration;
using OpenReferrals.RegisterManagementConnector.Exceptions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace OpenReferrals.RegisterManagementConnector.ServiceClients
{
    public class HttpClientAdapter : IHttpClientAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientAdapter> _logger;
        private readonly string _RegisterScope = string.Empty;
        private readonly string _UserScope = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public HttpClientAdapter(
            ILogger<HttpClientAdapter> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
            _RegisterScope = configuration["RegisterApi:Scopes"];
            _UserScope = configuration["UserApi:Scopes"];
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<string> RegisterGetRequest(Uri endpoint)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, endpoint);

            await AddAccessTokenToRequestHeader(message);

            var response = await _httpClient.SendAsync(message);
            var responseString = await response.Content.ReadAsStringAsync();

            if ((int)response.StatusCode >= 400)
            {
                var errorMessage = JsonConvert.DeserializeObject<RegisterError>(responseString);
                throw new RegisterClientException(response.StatusCode, errorMessage?.Error);
            }
            return responseString;
        }
        public async Task<string> RegisterPostRequest(Uri endpoint, object payload)
        {
            var message = new HttpRequestMessage();
            message.RequestUri = endpoint;
            message.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            message.Method = HttpMethod.Post;

            await AddAccessTokenToRequestHeader(message);
            var response = await _httpClient.SendAsync(message);
            var responseString = await response.Content.ReadAsStringAsync();

            if ((int)response.StatusCode >= 400)
            {
                var errorMessage = JsonConvert.DeserializeObject<RegisterError>(responseString);
                throw new RegisterClientException(response.StatusCode, errorMessage?.Error);
            }
            return responseString;
        }

        private async Task AddAccessTokenToRequestHeader(HttpRequestMessage message)
        {
            //string authority = $"https://login.microsoftonline.com/{_registerOptions.TenantID}";

            //var authContext = new AuthenticationContext(authority);
            //var credentials = new ClientCredential(_registerOptions.ClientId, _registerOptions.ClientSecret);

            //var token = await authContext.AcquireTokenAsync(_registerOptions.ResourceUri, credentials);
            //message.Headers.Add("Authorization", $"Bearer {token.AccessToken}");

            //var client = new HttpClient
            //{
            //    BaseAddress = new Uri($"https://login.microsoftonline.com/{_registerOptions.TenantID}/")
            //};
            //var content = new StringContent(
            //    JsonConvert.SerializeObject(
            //        new
            //        {
            //            client_id = _registerOptions.ClientId,
            //            client_secret = _registerOptions.ClientSecret,
            //            scope = _registerOptions.ResourceUri + "/.defualt",
            //            grant_type = "client_credentials"
            //        }), Encoding.UTF8, "application/json");

            //var response = await client.PostAsync("oauth2/v2.0/token", content);
            //var tokenResponse = await response.Content.ReadAsStringAsync();

            try
            {
                var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                accessToken = accessToken.ToString().Replace("Bearer", "").Trim();
                Debug.WriteLine($"access token-{accessToken}");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch(Exception _)
            {
                throw;
            }
        }
    }
}
