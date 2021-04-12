using Microsoft.AspNetCore.Mvc;
using OpenReferrals.Connectors.PostcodeConnector.ServiceClients;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.OpenReferral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IPostcodeServiceClient _postcodeServiceClient;

        public LocationsController(
            ILocationRepository locationRepository,
            IPostcodeServiceClient postcodeServiceClient)
        {
            _locationRepository = locationRepository;
            _postcodeServiceClient = postcodeServiceClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var locations =  _locationRepository.GetAll();
            return Ok(locations);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Location location)
        {
            var postcode = location.Physical_Addresses.First().Postal_Code;
            var locationResult = await _postcodeServiceClient.GetPostcodeLocation(postcode);

            location.Latitude = locationResult.Latitude;
            location.Longitude = locationResult.Longitude;

            await _locationRepository.InsertOne(location);
            return Accepted(location);
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