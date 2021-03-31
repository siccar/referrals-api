using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class ServiceAtLocation
    {
        public string Id { get; set; }
        public Location Location {get; set;}
        public Service Service { get; set; }
    }
}
