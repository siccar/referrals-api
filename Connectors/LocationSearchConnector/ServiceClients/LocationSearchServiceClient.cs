using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using OpenReferrals.Connectors.LocationSearchConnector.Models;
using OpenReferrals.Connectors.PostcodeConnector.ServiceClients;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Spatial;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.LocationSearchConnector.ServiceClients
{
    public class LocationSearchServiceClient : ILocationSearchServiceClient
    {

        private readonly ISearchIndexClient _searchIndexClient;
        private readonly IPostcodeServiceClient _postcodeServiceClient;

        public LocationSearchServiceClient(ISearchIndexClient searchIndexClient, IPostcodeServiceClient postcodeServiceClient)
        {
            _searchIndexClient = searchIndexClient;
            _postcodeServiceClient = postcodeServiceClient;
        }

        public void AddLocation(Location location)
        {
            var newLocation = new SearchLocation()
            {
                Id = location.Id,
                Name = location.Name,
                Point = GeographyPoint.Create(location.Latitude, location.Longitude),
                Physical_Addresses = location.Physical_Addresses
            };
            var batch = IndexBatch.Upload(newLocation);
            _searchIndexClient.Documents.Index(batch);
        }

        public async Task<DocumentSearchResult<SearchLocation>> QueryLocations(string postcode, int distance = 5)
        {
            var location = await _postcodeServiceClient.GetPostcodeLocation(postcode);
            var searchParameters = new SearchParameters()
            {
                Filter = $"geo.distance(Point, geography'POINT({location.Latitude} {location.Latitude})') le {distance}",
                SearchMode = SearchMode.Any,
            };

            var results = await _searchIndexClient.Documents.SearchAsync<SearchLocation>("*", searchParameters);
            throw new NotImplementedException();
        }
    }
}
