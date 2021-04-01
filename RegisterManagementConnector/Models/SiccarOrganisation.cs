using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.Models
{
    public class SiccarOrganisation
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public int CharityNumber { get; set; }
        public IEnumerable<string> ServicesIds { get; set; }
        public string Url { get; set; }
    }
}
