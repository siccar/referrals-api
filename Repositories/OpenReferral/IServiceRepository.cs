using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenReferrals.DataModels;

namespace OpenReferrals.Repositories.OpenReferral
{
    public interface IServiceRepository
    {
        Task InsertOne(Service ser);
        Task UpdateOne(Service ser);
        Task<Service> FindById(string serviceId);
        IEnumerable<Service> GetAll();

    }
}
