using Newtonsoft.Json;
using OpenReferrals.Connectors.Common;
using OpenReferrals.Connectors.PostcodeConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.PostcodeConnector.ServiceClients
{
    public class PostcodeServiceClient : IPostcodeServiceClient
    {
        private readonly IUnAuthenticatedHttpAdapter _httpClient;
        private const string PostcodeLocationBaseUrl = "https://api.postcodes.io/";

        public PostcodeServiceClient(IUnAuthenticatedHttpAdapter httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PostcodeLocation> GetPostcodeLocation(string postcode)
        {
            var responseString = await _httpClient.GetRequest(
                new Uri(PostcodeLocationBaseUrl + $"postcodes/{postcode}"));

            var postcodeLocation = JsonConvert.DeserializeObject<PostcodeResult>(responseString);
            return postcodeLocation.Result;
        }

        public async Task<PostcodeValidation> ValidatePostcode(string postcode)
        {
            var responseString = await _httpClient.GetRequest(
                new Uri(PostcodeLocationBaseUrl + $"postcodes/{postcode}/validate"));

            var isValid = JsonConvert.DeserializeObject<PostcodeValidation>(responseString);
            return isValid;
        }
    }
}
