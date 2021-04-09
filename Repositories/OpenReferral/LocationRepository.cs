using OpenReferrals.DataModels;
using OpenReferrals.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public class LocationRepository : ILocationRepository
    {
        private IMongoRepository<Location> _repo;

        public LocationRepository(IMongoRepository<Location> repo)
        {
            _repo = repo;
        }

        public async Task<Location> FindById(string organisationId)
        {
            return await _repo.FindByIdAsync(organisationId);
        }

        public IEnumerable<Location> GetAll()
        {
            return _repo.FilterBy(_ => true);
        }

        public async Task InsertOne(Location org)
        {
            await _repo.InsertOneAsync(org);
        }

        public async Task UpdateOne(Location org)
        {
            await _repo.ReplaceOneAsync(org);
        }
    }
}
