using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.Models
{
    public class SiccarOrganisation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string WebUrl { get; set; }
        public int Phone { get; set; }
        public int CharityNumber {get; set;}
        public List<string> Tags { get; set; }
        public List<string> Category { get; set; }
    }
}
