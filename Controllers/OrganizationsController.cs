using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Organisation> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IEnumerable<Organisation> Post()
        {
            throw new NotImplementedException();
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