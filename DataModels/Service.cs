using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;

namespace OpenReferrals.DataModels
{
    [BsonCollection("services")]
    public class Service : IMongoDocument
    {
        public string Description { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public IEnumerable<RegularSchedule> Regular_Schedules { get; set; }
        public IEnumerable<ServiceAtLocation> Service_At_Locations { get; set; }
        public IEnumerable<string> Contacts { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
    }
}
