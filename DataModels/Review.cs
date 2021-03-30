using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Review
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public Organisation Organization { get; set; }
        public string Score { get; set; }
        public Service Service {get; set;}
        public string Title { get; set; }
        public string Url { get; set; }
        public string Widget { get; set; }
    }
}
