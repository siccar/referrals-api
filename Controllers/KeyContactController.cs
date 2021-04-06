using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Models;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using OpenReferrals.Sevices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class KeyContactController : ControllerBase
    {

        private readonly IKeyContactRepository _keyContactRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        public KeyContactController(
            IKeyContactRepository keyContactRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient
            )
        {
            _keyContactRepository = keyContactRepository;
            _registerManagmentServiceClient = registerManagmentServiceClient;
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
            return Ok();
        }


        [HttpGet]
        [Route("admin/confirm/{orgId}/{userId}")]
        public async Task<IActionResult> AddAdminRequestToKeyContact([FromRoute] string orgId, [FromRoute] string userId)
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
        [Route("{orgId}")]
        public async Task<IActionResult> DeleteOrgKeyContacts([FromRoute] string orgId)
        {
            await _keyContactRepository.DeleteOne(new KeyContacts() { Id = Guid.NewGuid().ToString(), OrgId = orgId, UserId = JWTAttributesService.GetSubject(Request), UserEmail = JWTAttributesService.GetEmail(Request) });
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetKeyContacts()
        {
            // Get Token from header ??? 
            var response = _keyContactRepository.GetAll();
            return Ok(response);
        }


    }
}