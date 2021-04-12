using OpenReferrals.Connectors.PostcodeConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.PostcodeConnector.ServiceClients
{
    public interface IPostcodeServiceClient
    {
        Task<PostcodeLocation> GetPostcodeLocation(string postcode);
    }
}
