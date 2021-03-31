using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.ServiceClients
{
    public interface IHttpClientAdapter
    {
        Task<string> RegisterGetRequest(Uri endpoint);
        Task<string> RegisterPostRequest(Uri endpoint, object payload);
    }
}
