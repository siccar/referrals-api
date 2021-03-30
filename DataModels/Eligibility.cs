using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Eligibility
    {
        public string Eligibiliti { get; set; }
        public string Id { get; set; }
        public string LinkId { get; set; }
        public float Maximum_Age { get; set; }
        public float Minimum_Age { get; set; }
        public Service Service { get; set; }
        public IEnumerable<EligibilityTaxonomy> Taxonomys { get; set; }
    }
}
