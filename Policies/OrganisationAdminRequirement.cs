using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Policies
{
    public class OrganisationAdminRequirement : IAuthorizationRequirement
    {
    }

    public static class AuthzPolicyNames
    {
        public const string MustBeOrgAdmin = "MustBeOrgAdmin";
    }
}
