using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using OpenReferrals.Sendgrid;
using OpenReferrals.Sevices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    [Route("[controller]")]
    public class KeyContactController : ControllerBase
    {

        private readonly IKeyContactRepository _keyContactRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        private readonly ISendGridSender _sendgridSender;
        public KeyContactController(
            IKeyContactRepository keyContactRepository,
            IOrganisationRepository organisationRepo,
            IRegisterManagmentServiceClient registerManagmentServiceClient,
            ISendGridSender sendGridSender
            )
        {
            _keyContactRepository = keyContactRepository;
            _organisationRepository = organisationRepo;
            _registerManagmentServiceClient = registerManagmentServiceClient;
            _sendgridSender = sendGridSender;
        }

        [HttpGet]
        [Route("{orgId}")]
        public async Task<IActionResult> AddKeyContact([FromRoute] string orgId)
        {
            await _keyContactRepository.InsertOne(new KeyContacts() {Id = Guid.NewGuid().ToString(), OrgId = orgId, UserId = JWTAttributesService.GetSubject(Request), UserEmail = JWTAttributesService.GetEmail(Request) } );
            return Ok();
        }


        [HttpGet]
        [Route("admin/{orgId}")]
        public async Task<IActionResult> AddAdminRequestToKeyContact([FromRoute] string orgId)
        {
            await _keyContactRepository.InsertOne(new KeyContacts() { Id = Guid.NewGuid().ToString(), OrgId = orgId, UserId = JWTAttributesService.GetSubject(Request), UserEmail = JWTAttributesService.GetEmail(Request), IsAdmin = true, IsPending = true });

            var keyContacts = await _keyContactRepository.FindApprovedByOrgId(orgId);
            var org = await _organisationRepository.FindById(orgId);
            foreach (var kc in keyContacts)
            {
                await _sendgridSender.SendSingleTemplateEmail(
                new SendGrid.Helpers.Mail.EmailAddress("support@wearecast.org.uk"),
                new SendGrid.Helpers.Mail.EmailAddress(kc.UserEmail),
                org.Name
                );
            }
            return Ok();
        }


        [HttpGet]
        [Route("admin/confirm/{orgId}/{userId}")]
        public async Task<IActionResult> ApproveAdminRequest([FromRoute] string orgId, [FromRoute] string userId)
        {
            var contact = _keyContactRepository.GetAll().Where(x=>x.OrgId == orgId && x.UserId == userId && x.IsPending == true).FirstOrDefault();
            contact.IsPending = false;
            await _keyContactRepository.UpdateOne(contact);
            return Ok();
        }

        [HttpGet]
        [Route("admin/requests")]
        public async Task<IActionResult> GetPendingKeyContacts()
        {
            var callingUserId = JWTAttributesService.GetSubject(Request);
            var contacts = await _keyContactRepository.FindByUserId(callingUserId);
            var list = new List<KeyContacts>();
            foreach(var contact in contacts)
            {
                var  x = (await _keyContactRepository.FindByOrgId(contact.OrgId)).Where(x => x.UserId != callingUserId && x.IsPending == true);
                list.AddRange(x);
            }

            return Ok(list);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteOrgKeyContacts([FromBody] KeyContacts contact)
        {
            await _keyContactRepository.DeleteOne(contact);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetKeyContacts()
        {
            // Get Token from header ??? 
            var response = _keyContactRepository.GetAll();
            return Ok(response);
        }

        [HttpGet]
        [Route("orgs-i-can-manage")]
        public async Task<IActionResult> GetOrgsIAmAKeyContactFor()
        {
            var userId = JWTAttributesService.GetSubject(Request);
            var responnses = await _keyContactRepository.FindByUserId(userId);
            var nonPending =  responnses.Where(x => x.IsPending == false);
            return Ok(nonPending);
        }

        [HttpGet]
        [Route("orgs-i-have-requested-to-join")]
        public async Task<IActionResult> GetOrgsIAmRequestingToJoin()
        {
            var userId = JWTAttributesService.GetSubject(Request);
            var responnses = await _keyContactRepository.FindByUserId(userId);
            var nonPending = responnses.Where(x => x.IsPending == true);
            return Ok(nonPending);
        }
        

        [HttpGet]
        [Route("orgs/{orgId}/contacts")]
        public async Task<IActionResult> GetOrgsIAmAKeyContactFor(string orgId)
        {
            return Ok(await _keyContactRepository.FindByOrgId(orgId));
        }


    }
}