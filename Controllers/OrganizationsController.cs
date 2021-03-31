using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Models;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        public OrganizationsController(IRegisterManagmentServiceClient registerManagmentServiceClient)
        {
            _registerManagmentServiceClient = registerManagmentServiceClient;
        }

        [HttpGet]
        public IEnumerable<Organisation> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SiccarOrganisation organisation)
        {
            var result = await _registerManagmentServiceClient.CreateOrganisation(organisation);
            return Accepted(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<Organisation> Get(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        public IEnumerable<Organisation> Put(string id)
        {
            throw new NotImplementedException();
        }
    }
}