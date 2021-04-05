using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Models;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using OpenReferrals.Sevices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class OrganizationMemberController : ControllerBase
    {

        private readonly IOrganisationMemberRepository _orgRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        private readonly IKeyContactRepository _keyContactRepo;

        public OrganizationMemberController(
            IKeyContactRepository keyContactRepo,
            IOrganisationMemberRepository orgRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient
            )
        {
            _orgRepository = orgRepository;
            _keyContactRepo = keyContactRepo;
            _registerManagmentServiceClient = registerManagmentServiceClient;
        }

        [HttpGet]
        [Route("create/{orgId}")]
        public IActionResult CreateRequest([FromRoute] string orgId)
        {
            var orgmemberRequest = new OrganisationMember() { Id = Guid.NewGuid().ToString(), OrgId = orgId, Status = OrganisationMembersStatus.REQUESTED, UserId = JWTAttributesService.GetSubject(Request) };
            return Ok(_orgRepository.InsertOne(orgmemberRequest));
        }



        [HttpGet]
        [Route("pending/{orgId}")]
        public IActionResult GetAllPendingRequests([FromRoute] string orgId)
        {
            var keycontactUserId = JWTAttributesService.GetSubject(Request);
            return Ok(_orgRepository.GetAllPendingRequests(keycontactUserId));
        }

        [HttpGet]
        [Route("all/{orgId}")]
        public IActionResult GetALlMembers([FromRoute] string orgId)
        {
            var keycontactUserId = JWTAttributesService.GetSubject(Request);
            return Ok(_orgRepository.GetAllMembers(keycontactUserId));
        }


        [HttpGet]
        [Route("grant/{orgId}/{userId}")]
        public IActionResult GrantAccess([FromRoute] string orgId, [FromRoute] string userId)
        {
            return UpdateUser(Request, orgId, userId, OrganisationMembersStatus.JOINED);

        }

        [HttpGet]
        [Route("revoke/{orgId}/{userId}")]
        public IActionResult RevokeAccess([FromRoute] string orgId, [FromRoute] string userId)
        {
            return UpdateUser(Request, orgId, userId, OrganisationMembersStatus.REVOKED);
        }

        [HttpGet]
        [Route("deny/{orgId}/{userId}")]
        public IActionResult DenyAccess([FromRoute] string orgId, [FromRoute] string userId)
        {
            return UpdateUser(Request, orgId, userId, OrganisationMembersStatus.DENIED);
        }

        private IActionResult UpdateUser(HttpRequest request, string orgId, string userId, OrganisationMembersStatus status)
        {
            if (IsCallingUserIsKeyContact(request))
            {
                var orgmemberRequest = new OrganisationMember() { OrgId = orgId, Status = status, UserId = userId };
                return Ok(_orgRepository.UpdateOne(orgmemberRequest));
            }
            return new UnauthorizedObjectResult("Only Key contacts call call this method");
        }

        private bool IsCallingUserIsKeyContact(HttpRequest request)
        {
            var userId = JWTAttributesService.GetSubject(request);
            var exists = _keyContactRepo.FindByUserId(userId);
            return exists != null;
        }
    }
}