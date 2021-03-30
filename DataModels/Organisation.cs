using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Organisation
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public string Uri { get; set; }
        public string Url { get; set; }
    }
}
