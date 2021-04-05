using MongoDB.Driver;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public class KeyContactRepository : IKeyContactRepository
    {
        private IMongoRepository<KeyContacts> _repo;

        public KeyContactRepository(IMongoRepository<KeyContacts> repo)
        {
            _repo = repo;
        }

        public async Task<KeyContacts> FindByOrgId(string organisationId)
        {
            return await _repo.FindOneAsync(x => x.OrgId == organisationId);
        }

        public async Task<KeyContacts> FindByUserId(string userId)
        {
            return await _repo.FindOneAsync(x => x.UserId == userId);
        }

        public IEnumerable<KeyContacts> GetAll()
        {
            return _repo.FilterBy(_ => true);

        }

        public async Task InsertOne(KeyContacts contact)
        {
            await _repo.InsertOneAsync(contact);
        }

        public async Task DeleteOne(KeyContacts contact)
        {
            await _repo.DeleteOneAsync(x => x.OrgId == contact.OrgId && x.UserId == contact.UserId);
        }

        public async Task UpdateOne(KeyContacts contact)
        {
            await _repo.ReplaceOneAsync(contact);
        }
    }
}
