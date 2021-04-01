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

        public async Task<Organisation> CreateOrganisation(Organisation SiccarOrganisation)
        {
            // This is the point at which we'll send the data to Siccar
            // Siccar will create a group/wallet, add the user to the group, start the process, and submit the organisations data. 
            // We then use the process instanceId as the organisations id. 

            //TODO Convert to a SiccarOrganisation then publish to siccar
            //var endpoint = new Uri($"{_options.BaseUrl}/OpenReferrals");

            //var result = await _httpClient.RegisterPostRequest(endpoint, SiccarOrganisation);

            //return JsonConvert.DeserializeObject<SiccarOrganisation>(result);

            SiccarOrganisation.Id = Guid.NewGuid().ToString("N");
            return SiccarOrganisation;
        }
    }
}
