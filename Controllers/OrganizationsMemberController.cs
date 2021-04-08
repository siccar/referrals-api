using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Models;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using OpenReferrals.Sendgrid;
using OpenReferrals.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class OrganizationMemberController : ControllerBase
    {

        private readonly IOrganisationRepository _orgRepository;
        private readonly IOrganisationMemberRepository _orgMemberRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        private readonly IKeyContactRepository _keyContactRepo;
        private readonly ISendGridSender _sendgridSender;

        public OrganizationMemberController(
            IKeyContactRepository keyContactRepo,
            IOrganisationMemberRepository orgMemberRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient,
            ISendGridSender sendgridSender
            )
        {
            _orgMemberRepository = orgMemberRepository;
            _keyContactRepo = keyContactRepo;
            _registerManagmentServiceClient = registerManagmentServiceClient;
            _sendgridSender = sendgridSender;
        }

        [HttpGet]
        [Route("my/requests")]
        public async Task<IActionResult> FetchRequestsAboutMe()
        {
            var userId = JWTAttributesService.GetSubject(Request);
            return Ok(_orgMemberRepository.GetRequestsAboutUser(userId));
        }

        [HttpGet]
        [Route("admin/requests")]
        public async Task<IActionResult> FetchAdminRequests()
        {
            var userId = JWTAttributesService.GetSubject(Request);
            var orgs = await _keyContactRepo.FindByUserId(userId);
            var pendingrequests = new List<OrganisationMember>();
            foreach (var o in orgs)
            {
                if (!o.IsPending)
                {
                    pendingrequests.AddRange(_orgMemberRepository.GetAllPendingRequests(o.OrgId).Where(x => x.Status != OrganisationMembersStatus.JOINED && x.Status != OrganisationMembersStatus.DENIED));
                }

            }

            return Ok(pendingrequests);
        }

        [HttpGet]
        [Route("create/{orgId}")]
        public async Task<IActionResult> CreateRequest([FromRoute] string orgId)
        {
            // Restricts the requests so you only get one request per org per user ID
            var userId = JWTAttributesService.GetSubject(Request);
            var email = JWTAttributesService.GetEmail(Request);
            var existingRequestsForUser = _orgMemberRepository.GetRequestsAboutUser(userId).Where(x => x.OrgId == orgId);
            if (existingRequestsForUser.Count() == 0)
            {
                var orgmemberRequest = new OrganisationMember() { Id = Guid.NewGuid().ToString(), OrgId = orgId, Status = OrganisationMembersStatus.REQUESTED, UserId = userId, Email = email };
                await _orgMemberRepository.InsertOne(orgmemberRequest);
            }

            // Get Key Contact for org 
            var keyContacts = await _keyContactRepo.FindApprovedByOrgId(orgId);

            foreach (var kc in keyContacts)
            {
                await _sendgridSender.SendSingleTemplateEmail(
                new SendGrid.Helpers.Mail.EmailAddress("info@wallet.services"),
                new SendGrid.Helpers.Mail.EmailAddress(kc.UserEmail));
            }

            return Ok();
        }

        [HttpGet]
        [Route("pending/{orgId}")]
        public IActionResult GetAllPendingRequests([FromRoute] string orgId)
        {
            var requests = _orgMemberRepository.GetAllPendingRequests(orgId);
            return Ok(requests);
        }

        [HttpGet]
        [Route("all/joined")]
        public IActionResult GetAllMembers()
        {
            var requests = _orgMemberRepository.GetAllMembers();
            return Ok(requests);
        }

        [HttpGet]
        [Route("all/{orgId}")]
        public IActionResult GetALlMembers([FromRoute] string orgId)
        {
            var requests = _orgMemberRepository.GetAllMembers(orgId);
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
            if (await IsCallingUserIsKeyContact(request))
            {
                var orgmember = await _orgMemberRepository.Find(orgId, userId);
                orgmember.Status = status;
                await _orgMemberRepository.UpdateOne(orgmember);
                return Ok();
            }
            return new UnauthorizedObjectResult("Only Key contacts call call this method");
        }

        private async Task<bool> IsCallingUserIsKeyContact(HttpRequest request)
        {
            var userId = JWTAttributesService.GetSubject(request);
            var exists = (await _keyContactRepo.FindByUserId(userId)).ToList().Where(x => x.IsPending == false).ToList();
            return exists.Count > 0;
        }
    }
}