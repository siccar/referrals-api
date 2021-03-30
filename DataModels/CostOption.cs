using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class CostOption
    {
        public float Amount { get; set; }
        public string Amount_Description { get; set; }
        public string Id { get; set; }
        public string LinkId { get; set; }
        public string Option { get; set; }
        public Service Service { get; set; }
        public DateTime Valid_From { get; set; }
        public DateTime Valid_To { get; set; }
    }
}
