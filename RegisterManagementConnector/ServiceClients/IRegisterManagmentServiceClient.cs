using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.ServiceClients
{
    public interface IRegisterManagmentServiceClient
    {
        Task<Organisation> CreateOrganisation(Organisation organisation);
    }
}
