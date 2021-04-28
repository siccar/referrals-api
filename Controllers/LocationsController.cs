using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenReferrals.Connectors.LocationSearchConnector.ServiceClients;
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
        private readonly ILocationSearchServiceClient _locationSearchServiceClient;

        public LocationsController(
            ILocationRepository locationRepository,
            IPostcodeServiceClient postcodeServiceClient,
            ILocationSearchServiceClient locationSearchServiceClient
            )
        {
            _locationRepository = locationRepository;
            _postcodeServiceClient = postcodeServiceClient;
            _locationSearchServiceClient = locationSearchServiceClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var locations = _locationRepository.GetAll();
            return Ok(locations);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Location location, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                Guid.Parse(location.Id);
            }
            catch (Exception _)
            {
                ModelState.AddModelError(nameof(Location.Id), "Location Id is not a valid Guid");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var postcode = location.Physical_Addresses.First().Postal_Code;
            var isValid = await _postcodeServiceClient.ValidatePostcode(postcode);
            if(!isValid.Result)
            {
                ModelState.AddModelError(nameof(Organisation.Id), "Postcode does not exist.");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var locationResult = await _postcodeServiceClient.GetPostcodeLocation(postcode);

            location.Latitude = locationResult.Latitude;
            location.Longitude = locationResult.Longitude;

            _locationSearchServiceClient.AddLocation(location);
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

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] Location location)
        {
            await _locationRepository.UpdateOne(location);
            return Accepted();
        }
    }
}