using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LocationsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Location> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IEnumerable<Location> Post()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<Location> Get([FromRoute] string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        public IEnumerable<Location> Put([FromRoute] string id)
        {
            throw new NotImplementedException();
        }
    }
}