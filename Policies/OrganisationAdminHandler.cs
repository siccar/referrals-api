using Microsoft.AspNetCore.Authorization;
using OpenReferrals.Repositories.OpenReferral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenReferrals.Policies
{
    public class OrganisationAdminHandler : AuthorizationHandler<OrganisationAdminRequirement, string>
    {
        private readonly IKeyContactRepository _keyContactRepository;
        public OrganisationAdminHandler(IKeyContactRepository keyContactRepository)
        {
            _keyContactRepository = keyContactRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                  OrganisationAdminRequirement requirement,
                                                  string orgId)
        {
            var keyContacts = _keyContactRepository.FindByOrgId(orgId);
            var subClaim = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

            if (keyContacts.Result.ToList().Where(m => m.UserId == subClaim?.Value).Any())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
