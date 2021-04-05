using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Configuration;
using OpenReferrals.RegisterManagementConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.ServiceClients
{
    public class RegisterManagementServiceClient : IRegisterManagmentServiceClient
    {
        private readonly RegisterManagmentOptions _options;
        private readonly IHttpClientAdapter _httpClient;
        private readonly IConfiguration _config;
        public RegisterManagementServiceClient(RegisterManagmentOptions options, IHttpClientAdapter httpClient, IConfiguration config)
        {
            _options = options;
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<Organisation> CreateOrganisation(Organisation organisation)
        {
            // This is the point at which we'll send the data to Siccar
            // Siccar will create a group/wallet, add the user to the group, start the process, and submit the organisations data. 
            // We then use the process instanceId as the organisations id. 

            //TODO Convert to a SiccarOrganisation then publish to siccar
            var endpoint = new Uri($"{_config["RegisterAPI:BaseUrl"]}/OpenReferrals");
            var siccarOrg = new SiccarOrganisation(organisation);

            var result = await _httpClient.RegisterPostRequest(endpoint, siccarOrg);

            return JsonConvert.DeserializeObject<Organisation>(result);

            //var value = await _downStreamWebApi.CallWebApiForUserAsync<SiccarOrganisation, Organisation>(
            //     "RegisterAPI",
            //     siccarOrg,
            //     options =>
            //     {
            //         options.HttpMethod = HttpMethod.Post;
            //         options.RelativePath = $"OpenReferrals";
            //     });

            //organisation.Id = Guid.NewGuid().ToString("N");
            //return value;
        }
    }
}
