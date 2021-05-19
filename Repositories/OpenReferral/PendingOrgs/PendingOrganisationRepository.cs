using MongoDB.Driver;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral.PendingOrgs
{
    public class PendingOrganisationRepository : IPendingOrganisationRepository
    {
        private IMongoRepository<PendingOrganisation> _repo;

        public PendingOrganisationRepository(IMongoRepository<PendingOrganisation> repo)
        {
            _repo = repo;
        }

        public async Task<PendingOrganisation> FindById(string organisationId)
        {
            return await _repo.FindByIdAsync(organisationId);
        }

        public IQueryable<PendingOrganisation> GetAll()
        {
            return _repo.AsQueryable();
        }

        public async Task InsertOne(PendingOrganisation org)
        {
            await _repo.InsertOneAsync(org);
        }

        public async Task UpdateOne(PendingOrganisation org)
        {
            await _repo.ReplaceOneAsync(org);
        }

        public async Task DeleteOne(PendingOrganisation org)
        {
            await _repo.DeleteOneAsync(pendingOrg => org.Id == pendingOrg.Id);
        }
    }
}
