using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.Common;

namespace OpenReferrals.Repositories.OpenReferral
{
    public class ServiceRepository : IServiceRepository
    {
        private IMongoRepository<Service> _repo;

        public ServiceRepository(IMongoRepository<Service> repo)
        {
            _repo = repo;
        }

        public async Task<Service> FindById(string serviceId)
        {
            return await _repo.FindByIdAsync(serviceId);
        }

        public IEnumerable<Service> GetAll()
        {
            return _repo.FilterBy(_ => true);
        }

        public async Task InsertOne(Service ser)
        {
            await _repo.InsertOneAsync(ser);
        }

        public async Task UpdateOne(Service ser)
        {
            await _repo.ReplaceOneAsync(ser);
        }
    }
}
