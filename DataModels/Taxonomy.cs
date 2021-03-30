using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Taxonomy
    {
        public string Id { get; set; }
        public IEnumerable<LinkTaxonomy> LinkTaxonomyCollection { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; } // to do: to be defined later
        public IEnumerable<ServiceTaxonomy> ServiceTaxonomyCollection { get; set; }
        public string Vocabulary { get; set; }
    }
}
