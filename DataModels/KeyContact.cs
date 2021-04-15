using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    [BsonCollection("keycontacts")]
    public class KeyContacts : IMongoDocument
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string OrgId  { get; set; }
        [Required]
        public string UserId  { get; set; }
        public string UserEmail { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public bool IsPending { get; set; }
    }
}
