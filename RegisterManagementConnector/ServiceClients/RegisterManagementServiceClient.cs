using Newtonsoft.Json;
using OpenReferrals.DataModels;
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

        public async Task<Organisation> CreateOrganisation(Organisation organisation)
        {
            // This is the point at which we'll send the data to Siccar
            // Siccar will create a group/wallet, add the user to the group, start the process, and submit the organisations data. 
            // We then use the process instanceId as the organisations id. 

            //TODO Convert to a SiccarOrganisation then publish to siccar
            //var endpoint = new Uri($"{_options.BaseUrl}/OpenReferrals");
            //var siccarOrg = new SiccarOrganisation(organisation);

            //var result = await _httpClient.RegisterPostRequest(endpoint, organisation);

            //return JsonConvert.DeserializeObject<Organisation>(result);

            organisation.Id = Guid.NewGuid().ToString("N");
            return organisation;
        }
    }
}
