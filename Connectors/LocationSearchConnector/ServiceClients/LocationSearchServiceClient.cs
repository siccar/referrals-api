using GeoJSON.Net.Geometry;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using OpenReferrals.Connectors.LocationSearchConnector.Models;
using OpenReferrals.Connectors.PostcodeConnector.ServiceClients;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            // The lat and long of the position Class need to be reversed to get them stored correctly
            var position = new Position(location.Longitude, location.Latitude);
            var newLocation = new SearchLocation()
            {
                id = location.Id,
                Name = location.Name,
                Point = new Point(position),
                Physical_Addresses = location.Physical_Addresses
            };

            var batch = IndexBatch.Upload(new List<SearchLocation> { newLocation });
            _searchIndexClient.Documents.Index(batch);
        }

        public async Task<IEnumerable<SearchLocation>> QueryLocations(string postcode, double? distance)
        {
            double kilometers = 5;

            if (distance != null)
            {
                kilometers = (double)distance / 0.62437;
            }

            var location = await _postcodeServiceClient.GetPostcodeLocation(postcode);
            var searchParameters = new SearchParameters()
            {
                Filter = $"geo.distance(Point, geography'POINT({location.Latitude} {location.Longitude})') le {Math.Round(kilometers, 2)}",
                SearchMode = SearchMode.Any,
            };
            try
            {
                var results = await _searchIndexClient.Documents.SearchAsync<SearchLocation>("*", searchParameters);
                var locations = results.Results.Select(r => r.Document);
                return locations;
            }
            catch (Exception _)
            {
                throw;
            }
        }
    }
}
