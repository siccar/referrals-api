using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    [BsonCollection("keycontacts")]
    public class KeyContacts : IMongoDocument
    {
        public string Id { get; set; }

        public string OrgId  { get; set; }

        public string UserId  { get; set; }
        public string UserEmail { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsPending { get; set; }
    }
}
