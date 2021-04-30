using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenReferrals.DataModels
{
    [BsonCollection("locations")]
    public class Location : IMongoDocument
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsVulnerable { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<PhysicalAddress> Physical_Addresses { get; set; }
    }
}