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
        [Route("my/requests")]
        public async Task<IActionResult> FetchRequestsAboutMe()
        {
            var userId = JWTAttributesService.GetSubject(Request);
            return Ok(_orgRepository.GetRequestsAboutUser(userId));
        }

        [HttpGet]
        [Route("admin/requests")]
        public async Task<IActionResult> FetchAdminRequests()
        {
            var userId = JWTAttributesService.GetSubject(Request);
            var org = await _keyContactRepo.FindByUserId(userId);
            return Ok(_orgRepository.GetAllPendingRequests(org.OrgId));
        }

        [HttpGet]
        [Route("create/{orgId}")]
        public async Task<IActionResult> CreateRequest([FromRoute] string orgId)
        {
            var orgmemberRequest = new OrganisationMember() { Id = Guid.NewGuid().ToString(), OrgId = orgId, Status = OrganisationMembersStatus.REQUESTED, UserId = JWTAttributesService.GetSubject(Request) };
            await _orgRepository.InsertOne(orgmemberRequest);
            return Ok();
        }

        [HttpGet]
        [Route("pending/{orgId}")]
        public IActionResult GetAllPendingRequests([FromRoute] string orgId)
        {
            var requests = _orgRepository.GetAllPendingRequests(orgId);
            return Ok(requests);
        }

        [HttpGet]
        [Route("all/{orgId}")]
        public IActionResult GetALlMembers([FromRoute] string orgId)
        {            
            var requests = _orgRepository.GetAllMembers(orgId);
            return Ok(requests);
        }


        [HttpGet]
        [Route("grant/{orgId}/{userId}")]
        public async Task<IActionResult> GrantAccess([FromRoute] string orgId, [FromRoute] string userId)
        {
            return await UpdateUser(Request, orgId, userId, OrganisationMembersStatus.JOINED);

        }

        [HttpGet]
        [Route("revoke/{orgId}/{userId}")]
        public async Task<IActionResult> RevokeAccess([FromRoute] string orgId, [FromRoute] string userId)
        {
            return await UpdateUser(Request, orgId, userId, OrganisationMembersStatus.REVOKED);
        }

        [HttpGet]
        [Route("deny/{orgId}/{userId}")]
        public async Task<IActionResult> DenyAccess([FromRoute] string orgId, [FromRoute] string userId)
        {
            return await UpdateUser(Request, orgId, userId, OrganisationMembersStatus.DENIED);
        }

        private async Task<IActionResult> UpdateUser(HttpRequest request, string orgId, string userId, OrganisationMembersStatus status)
        {
            if (IsCallingUserIsKeyContact(request))
            {
                var orgmember = await _orgRepository.Find(orgId, userId);
                orgmember.Status = status;
                await  _orgRepository.UpdateOne(orgmember);
                return Ok();
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