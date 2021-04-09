using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.OpenReferral;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        public ILocationRepository _locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var locations =  _locationRepository.GetAll();
            return Ok(locations);
        }

        [HttpPost]
        public async  Task<IActionResult> Post(Location location)
        {
            location.Id = Guid.NewGuid().ToString();
            await _locationRepository.InsertOne(location);
            return Accepted();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var location = await _locationRepository.FindById(id);
            return Ok(location);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] Location location)
        {
            await _locationRepository.UpdateOne(location);
            return Accepted();
        }
    }
}