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

        [HttpPost]
        [Route("{orgId}")]
        public async Task<IActionResult> AddKeyContact([FromRoute] string orgId)
        {
            await _keyContactRepository.InsertOne(new KeyContacts() {Id = Guid.NewGuid().ToString(), OrgId = orgId, UserId = JWTAttributesService.GetSubject(Request), UserEmail = JWTAttributesService.GetEmail(Request) } );
            return Ok();
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