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
        public string Accreditations { get; set; }
        public DateTime Assured_Date { get; set; }
        public string Attending_Access { get; set; }
        public string Attending_Type { get; set; }
        //currently limited to one
        public IEnumerable<string> Contacts { get; set; }
        public string Deliverable_Type { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Fees { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public IEnumerable<string> Regular_Schedules_Ids { get; set; }
        public IEnumerable<string> Service_At_Locations_Ids { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
    }
}
