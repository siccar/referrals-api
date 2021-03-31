using Newtonsoft.Json;
using OpenReferrals.RegisterManagementConnector.Configuration;
using OpenReferrals.RegisterManagementConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.ServiceClients
{
    public class RegisterManagementServiceClient : IRegisterManagmentServiceClient
    {
        private readonly RegisterManagmentOptions _options;
        private readonly IHttpClientAdapter _httpClient;
        public RegisterManagementServiceClient(RegisterManagmentOptions options, IHttpClientAdapter httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public async Task<SiccarOrganisation> CreateOrganisation(SiccarOrganisation SiccarOrganisation)
        {
            var endpoint = new Uri($"{_options.BaseUrl}/OpenReferrals");

            var result = await _httpClient.RegisterPostRequest(endpoint, SiccarOrganisation);

            return JsonConvert.DeserializeObject<SiccarOrganisation>(result);
        }
    }
}
