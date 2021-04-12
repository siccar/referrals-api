using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.Common
{
    public interface IHttpClientAdapter
    {
        Task<string> GetRequest(Uri endpoint);
        Task<string> PostRequest(Uri endpoint, object payload);
    }
}
