using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.RegisterManagementConnector.Models
{
    public class SiccarOrganisation
    {
        public SiccarOrganisation(Organisation org)
        {
            Id = org.Id;
            Description = org.Description;
            Name = org.Name;
            CharityNumber = org.CharityNumber;
            ServicesIds = org.ServicesIds;
            Url = org.Url;

        }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int CharityNumber { get; set; }
        public IEnumerable<string> ServicesIds { get; set; }
        public string Url { get; set; }
    }
}
