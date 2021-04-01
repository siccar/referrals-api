using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.Models
{

    public interface IMongoDocument
    {
        String Id { get; set; }
    }
}
