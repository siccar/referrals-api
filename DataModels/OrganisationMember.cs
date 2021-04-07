using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    [BsonCollection("organisationmembers")]

    public class OrganisationMember : IMongoDocument
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string OrgId { get; set; }

        public string Email { get; set; }
        public OrganisationMembersStatus Status { get; set; }
    }
}
