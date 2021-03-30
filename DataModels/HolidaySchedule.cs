using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class HolidaySchedule
    {
        public bool Closed { get; set; }
        public DateTime Closes_At { get; set; }
        public DateTime End_Date { get; set; }
        public string Id { get; set; }
        public DateTime Opens_At { get; set; }
        public Service Service { get; set; }
        public ServiceAtLocation Service_At_Location { get; set; }
        public DateTime Start_Date { get; set; }
    }
}
