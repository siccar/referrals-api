using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class ServiceArea
    {
        public string Extent { get; set; }
        public string Id { get; set; }
        public string LinkId { get; set; }
        public Service ServiceId { get; set; }
        public string Service_Area { get; set; }
        public string Uri { get; set; }
    }
}
