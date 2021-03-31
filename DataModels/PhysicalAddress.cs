using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class PhysicalAddress
    {
        public string Address_1 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Id { get; set; }
        public string LocationId { get; set; }
        public string Postal_Code { get; set; }
        public string State_Province { get; set; }
    }
}
