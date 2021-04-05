using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public interface IOrganisationMemberRepository
    {
        Task InsertOne(OrganisationMember member);
        Task UpdateOne(OrganisationMember member);
        IEnumerable<OrganisationMember> GetAllMembers(string orgId);

        IEnumerable<OrganisationMember> GetAllPendingRequests(string orgId);

        Task<OrganisationMember> Find(string orgId, string userId);
    }
}
