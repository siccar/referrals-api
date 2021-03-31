using System.Collections.Generic;

namespace OpenReferrals.DataModels
{
    public class Location
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Physical_Addresses_Ids { get; set; }
        public IEnumerable<string> Service_At_Locations_Ids { get; set; }
    }
}