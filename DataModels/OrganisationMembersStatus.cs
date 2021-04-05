using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{

    public enum OrganisationMembersStatus
    {
        /// <summary>
        /// First status 
        ///     Users request to join 
        ///     Tthen if the org denies them it is set to Denied
        ///     Otherwise Member is allowed to join
        ///     At the end the user membership can be revoked
        /// </summary>
        REQUESTED,
        DENIED,
        JOINED,
        REVOKED
    }
}
