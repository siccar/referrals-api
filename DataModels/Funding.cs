using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Funding
    { 
        public string Id { get; set; }
        public Service Service { get; set; }
        public string Source { get; set; }
    }
}
