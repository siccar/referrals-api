using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenReferrals.Connectors.LocationSearchConnector.ServiceClients;
using OpenReferrals.Connectors.PostcodeConnector.ServiceClients;
using OpenReferrals.DataModels;
using OpenReferrals.Policies;
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
        private readonly IServiceRepository _serviceRepository;
        private readonly IPostcodeServiceClient _postcodeServiceClient;
        private readonly ILocationSearchServiceClient _locationSearchServiceClient;
        private readonly IAuthorizationService _authorizationService;

        public LocationsController(
            ILocationRepository locationRepository,
            IServiceRepository serviceRepository,
            IPostcodeServiceClient postcodeServiceClient,
            ILocationSearchServiceClient locationSearchServiceClient,
            IAuthorizationService authorizationService
            )
        {
            _locationRepository = locationRepository;
            _serviceRepository = serviceRepository;
            _postcodeServiceClient = postcodeServiceClient;
            _locationSearchServiceClient = locationSearchServiceClient;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var locations = _locationRepository.GetAll();
            var locationIds = locations.Select(res => res.Id);

            var authorisedLocations = new List<Location>();
            var services = _serviceRepository.GetAll();
            foreach (var location in locations)
            {
                var locationServices = services.ToList()
                    .FindAll(service => locationIds.Contains(service.Service_At_Locations.First().Location_Id));

                if (location.IsVulnerable)
                {
                    if (await UserIsAuthorisedForLocationAsync(location)) { authorisedLocations.Add(location); }

                }
                else
                {
                    authorisedLocations.Add(location);
                }

            }

            return Ok(authorisedLocations);
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
            if (!isValid.Result)
            {
                ModelState.AddModelError(nameof(Organisation.Id), "Postcode does not exist.");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var locationResult = await _postcodeServiceClient.GetPostcodeLocation(postcode);

            location.Latitude = locationResult.Latitude;
            location.Longitude = locationResult.Longitude;

            _locationSearchServiceClient.AddOrUpdateLocation(location);
            await _locationRepository.InsertOne(location);
            return Accepted(location);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                Guid.Parse(id);
            }
            catch (Exception _)
            {
                ModelState.AddModelError(nameof(Location.Id), "Location Id is not a valid Guid");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var location = await _locationRepository.FindById(id);

            var userIsAuthorised = await UserIsAuthorisedForLocationAsync(location);
            if (location.IsVulnerable && !userIsAuthorised)
            {
                return Forbid();
            }

            return Ok(location);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] Location location, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
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

            var userIsAuthorised = await UserIsAuthorisedForLocationAsync(location);

            if (!userIsAuthorised)
            {
                return Forbid();
            }

            var postcode = location.Physical_Addresses.First().Postal_Code;
            var isValid = await _postcodeServiceClient.ValidatePostcode(postcode);
            if (!isValid.Result)
            {
                ModelState.AddModelError(nameof(Organisation.Id), "Postcode does not exist.");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var locationResult = await _postcodeServiceClient.GetPostcodeLocation(postcode);

            location.Latitude = locationResult.Latitude;
            location.Longitude = locationResult.Longitude;

            _locationSearchServiceClient.AddOrUpdateLocation(location);
            await _locationRepository.UpdateOne(location);
            return Accepted(location);
        }

        private async Task<bool> UserIsAuthorisedForLocationAsync(Location location)
        {
            var services = _serviceRepository.GetAll()
                .Where(service => service.Service_At_Locations.Any(serviceLocation => serviceLocation.Location_Id == location.Id)).ToList();

            var userIsAuthorised = false;
            foreach (var service in services)
            {
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, service.OrganizationId, AuthzPolicyNames.MustBeOrgAdmin);
                if (authorizationResult.Succeeded)
                {
                    userIsAuthorised = true;
                    break;
                }

            }

            return userIsAuthorised;
        }
    }
}