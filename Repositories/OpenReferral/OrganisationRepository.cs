using MongoDB.Driver;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private IMongoRepository<Organisation> _repo;

        public OrganisationRepository(IMongoRepository<Organisation> repo)
        {
            _repo = repo;
        }

        public async Task<Organisation> FindById(string organisationId)
        {
            return await _repo.FindByIdAsync(organisationId);
        }

        public IEnumerable<Organisation> GetAll()
        {
            return _repo.FilterBy(_ => true);
        }

        public async Task InsertOne(Organisation org)
        {
            await _repo.InsertOneAsync(org);
        }

        public async Task UpdateOne(Organisation org)
        {
            await _repo.ReplaceOneAsync(org);
        }
    }
}
