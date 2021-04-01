using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.Models
{
    public class MongoDocument : IMongoDocument
    {
        public String Id { get; set; }

        public string DocType { get; }
    }
}
