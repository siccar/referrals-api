using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class ServiceTaxonomy
    {
        public string Id { get; set; }
        public string LinkId { get; set; }
        public Service Service { get; set; }
        public Taxonomy Taxonomy {get; set;} 
    }
}
