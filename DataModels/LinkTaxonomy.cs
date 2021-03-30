using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class LinkTaxonomy
    {
        public string Id { get; set; }
        public string Link_Id { get; set; }
        public string Link_Type { get; set; }
        public Taxonomy Taxonomy { get; set; }
    }
}
