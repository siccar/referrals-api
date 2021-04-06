using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public interface IKeyContactRepository
    {
        Task InsertOne(KeyContacts contact);

        Task DeleteOne(KeyContacts contact);
        Task UpdateOne(KeyContacts contact);
        Task<IEnumerable<KeyContacts>> FindByOrgId(string organisationId);

        Task<IEnumerable<KeyContacts>> FindByUserId(string userId);
       IEnumerable<KeyContacts> GetAll();
    }
}
