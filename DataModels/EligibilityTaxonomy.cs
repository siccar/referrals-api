using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class EligibilityTaxonomy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Taxonomy Parent {get; set;}
        public string Vocabulary { get; set; }
    }
}
