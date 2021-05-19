using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral.PendingOrgs
{
    public interface IPendingOrganisationRepository
    {
        Task InsertOne(PendingOrganisation org);
        Task UpdateOne(PendingOrganisation org);
        Task<PendingOrganisation> FindById(string organisationId);
        IQueryable<PendingOrganisation> GetAll();
        Task DeleteOne(PendingOrganisation org);
    }
}
