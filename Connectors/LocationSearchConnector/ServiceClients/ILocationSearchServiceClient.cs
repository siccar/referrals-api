using Microsoft.Azure.Search.Models;
using OpenReferrals.Connectors.LocationSearchConnector.Models;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.LocationSearchConnector.ServiceClients
{
    public interface ILocationSearchServiceClient
    {
        public Task<DocumentSearchResult<SearchLocation>> QueryLocations(string postcode, int distance);
        public void AddLocation(Location location);
    }
}
