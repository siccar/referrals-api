using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Models;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {

        private readonly IOrganisationRepository _orgRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        public OrganizationsController(
            IOrganisationRepository orgRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient
            )
        {
            _orgRepository = orgRepository;
            _registerManagmentServiceClient = registerManagmentServiceClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var orgs = _orgRepository.GetAll();
            return Ok(orgs);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Organisation organisation)
        {
            var publishedOrg = await _registerManagmentServiceClient.CreateOrganisation(organisation);
            await _orgRepository.InsertOne(publishedOrg);
            return Accepted(publishedOrg);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var org = await _orgRepository.FindById(id);
            return Ok(org);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] Organisation organisation)
        {
            await _orgRepository.UpdateOne(organisation);
            return Ok();
        }
    }
}