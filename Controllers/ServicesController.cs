using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Service> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Service Post()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public Service Get(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        public Service Put(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}/richness")]
        public Ratings GetRichness(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}/validate")]
        public string Validate (string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("richness")]
        public IEnumerable<Ratings> RichnessSelectionServices(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("richness")]
        public Ratings RichnessSingleServices(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("validate")]
        public string ValidateJsonService (string id)
        {
            throw new NotImplementedException();
        }
    }
}