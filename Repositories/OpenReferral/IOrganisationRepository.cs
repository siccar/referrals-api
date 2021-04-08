using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public interface IOrganisationRepository
    {
        Task InsertOne(Organisation org);
        Task UpdateOne(Organisation org);
        Task<Organisation> FindById(string organisationId);
        IQueryable<Organisation> GetAll();
    }
}
