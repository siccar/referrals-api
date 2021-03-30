using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class ServiceAtLocation
    {
        public IEnumerable<HolidaySchedule> HolidayScheduleCollection { get; set; }
        public string Id { get; set; }
        public Location Location {get; set;}
        public RegularSchedule Regular_Schedule { get; set; }
        public Service Service { get; set; }
    }
}
