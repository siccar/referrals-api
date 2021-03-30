using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Service
    {
        public string Accreditations { get; set; }
        public DateTime Assured_Date { get; set; }
        public string Attending_Access { get; set; }
        public string Attending_Type { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<CostOption> CostOption { get; set; }
        public string Deliverable_Type { get; set; }
        public string Description { get; set; }
        public IEnumerable<Eligibility> Eligibilitys { get; set; }
        public string Email { get; set; }
        public string Fees { get; set; }
        public IEnumerable<Funding> Fundings { get; set; }
        public IEnumerable<HolidaySchedule> Holiday_Schedules { get; set; }
        public string Id { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public string Name { get; set; }
        public Organisation Organization { get; set; }
        public IEnumerable<RegularSchedule> Regular_Schedules { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<ServiceArea> Service_Areas { get; set; }
        public IEnumerable<ServiceAtLocation> Service_At_Locations { get; set; }
        public IEnumerable<ServiceTaxonomy> Service_Taxonomys { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
    }
}
