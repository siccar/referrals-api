using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenReferrals.DataModels
{
    [BsonCollection("locations")]
    public class Location : IMongoDocument
    {
        public string Description { get; set; }
        [Required]
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<PhysicalAddress> Physical_Addresses { get; set; }
    }
}