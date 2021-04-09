using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public interface ILocationRepository
    {
        Task InsertOne(Location org);
        Task UpdateOne(Location org);
        Task<Location> FindById(string organisationId);
        IEnumerable<Location> GetAll();
    }
}
