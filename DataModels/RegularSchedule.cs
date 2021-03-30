using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class RegularSchedule
    {
        public string ByDay { get; set; }
        public string ByMonthDay { get; set; }
        public DateTime Closes_At { get; set; }
        public string Description { get; set; }
        public string DtStart { get; set; }
        public string Freq { get; set; }
        public string Id { get; set; }
        public string Interval { get; set; }
        public DateTime Opens_At { get; set; }
        public Service Service { get; set; }
        public ServiceAtLocation Service_At_Location { get; set; }
        public DateTime Valid_From { get; set; }
        public DateTime Valid_To { get; set; }
    }
}
