using System.Collections.Generic;

namespace OpenReferrals.DataModels
{
    public class Location
    {
        public AccessibilityForDisabilities Accessibility_For_Dissabilities { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public IEnumerable<PhysicalAddress> Physical_Addresses { get; set; }
        public IEnumerable<ServiceAtLocation> Service_At_Locations { get; set; }
    }
}