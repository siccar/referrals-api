using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.RegisterManagementConnector.Models
{
    public class SiccarService
    {
        public SiccarService(Service service)
        {
            Description = service.Description;
            Email = service.Email;
            Id = service.Id;
            Name = service.Name;
            OrganizationId = service.OrganizationId;
            Regular_Schedules = service.Regular_Schedules;
            Service_At_Locations = service.Service_At_Locations;
            Contacts = service.Contacts;
            Status = service.Status;
            Url = service.Url;
        }
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
