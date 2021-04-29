using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    [BsonCollection("organisations")]
    public class Organisation : IMongoDocument
    {
        [Required]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description must be populated")]
        [StringLength(200, ErrorMessage = "Description can only be 200 chars long.")]
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be populated")]
        [StringLength(100, ErrorMessage = "Name can only be 100 chars long.")]
        public string Name { get; set; }
        public int CharityNumber { get; set; }
        public IEnumerable<string> ServicesIds { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "URL must be populated")]
        [Url(ErrorMessage = "URL must be valid")]
        public string Url { get; set; }
    }
}
