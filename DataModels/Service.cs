using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;

namespace OpenReferrals.DataModels
{
    [BsonCollection("services")]
    public class Service : IMongoDocument
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description must be populated")]
        [StringLength(200, ErrorMessage = "Description can only be 200 chars long.")]
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email must be populated")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }
        [Required]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be populated")]
        [StringLength(200, ErrorMessage = "Name can only be 200 chars long.")]
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public IEnumerable<RegularSchedule> Regular_Schedules { get; set; }
        public IEnumerable<ServiceAtLocation> Service_At_Locations { get; set; }
        public IEnumerable<string> Contacts { get; set; }
        public string Status { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "URL must be populated")]
        [Url(ErrorMessage = "URL must be valid")]
        public string Url { get; set; }
        public IEnumerable<int> Tags { get; set; }
        public int? Category { get; set; }
    }
}
