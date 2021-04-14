using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using OpenReferrals.Repositories;
using Microsoft.AspNetCore.Authorization;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using System.Threading.Tasks;
using OpenReferrals.Connectors.LocationSearchConnector.ServiceClients;
using System.Linq;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        private readonly ILocationSearchServiceClient _locationSearchServiceClient;
        public ServicesController(
            IServiceRepository serRepository,
            ILocationRepository locationRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient,
            ILocationSearchServiceClient locationSearchServiceClient
            )
        {
            _serRepository = serRepository;
            _locationRepository = locationRepository;
            _registerManagmentServiceClient = registerManagmentServiceClient;
            _locationSearchServiceClient = locationSearchServiceClient;
        }

        /// <summary>
        /// Get All Services
        /// </summary>
        /// <param name="postcode">Use text to perform a keyword search on services. This performs a full text search on the service title.</param>
        /// <param name="text">The postcode of the person who wishes to use the service. In order to find services that are within a reasonable distance.</param>
        /// <param name="proximity">The distance in metres that the person is willing to travel from the target postcode.</param>
        /// <returns>A <see cref="List{Service}"/>Returns all services based on input parameters</returns>
        [HttpGet]
        public async Task<IActionResult> Get(string postcode = null, double? proximity = null, string text = null)
        {
            var services = _serRepository.GetAll();

            if (postcode != null)
            {
                var locationResults = await _locationSearchServiceClient.QueryLocations(postcode, proximity);
                var locationIds = locationResults.Select(res => res.id);
                //TODO: We only check for the first serviceAtLocation as we only save one, for now.
                services = services.ToList()
                    .FindAll(service => locationIds.Contains(service.Service_At_Locations.First().Location_Id));
            }

            if(text != null)
            {
                services = services.ToList().FindAll(service => service.Name.Contains(text));
            }

            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Service service)
        {
            var publishedService = _registerManagmentServiceClient.CreateService(service);
            await _serRepository.InsertOne(publishedService);
            return Accepted(publishedService);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var service = await _serRepository.FindById(id);
            return Ok(service);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] Service service)
        {
            var updatedService = _registerManagmentServiceClient.UpdateService(service);

            await _serRepository.UpdateOne(updatedService);
            return Ok(updatedService);
        }

        [HttpGet]
        [Route("{id}/validate")]
        public string Validate(string id)
        {
            ////what is meant to return? ask away
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("validate")]
        public string ValidateJsonService(string id)
        {
            //what is meant to return? ask away
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("richness")]
        public async Task<IActionResult> GetRichnessServicesSelection()
        {
            throw new NotImplementedException(); //todo: find out what the selection is based on
        }

        [HttpGet]
        [Route("{id}/richness")]
        public async Task<IActionResult> GetRichnessSingleService()
        {
            throw new NotImplementedException(); //todo: find out what this is under the proper data models
        }
    }
}