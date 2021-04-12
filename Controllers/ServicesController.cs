using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using OpenReferrals.Repositories;
using Microsoft.AspNetCore.Authorization;
using OpenReferrals.RegisterManagementConnector.Models;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using System.Threading.Tasks;
namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        public ServicesController(
            IServiceRepository serRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient
            )
        {
            _serRepository = serRepository;
            _registerManagmentServiceClient = registerManagmentServiceClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var services = _serRepository.GetAll();
            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Service service)
        {
            //todo: uncomment later on
            //var publishedService = _registerManagmentServiceClient.CreateService(service);
            await _serRepository.InsertOne(service);
            return Accepted(service);
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
            //todo: connect with Siccar later on
            //This does nothing when SiccarConnect flag is false
           // _registerManagmentServiceClient.CreateService(service);

            await _serRepository.UpdateOne(service);
            return Ok(service);
        }

        [HttpGet]
        [Route("{id}/validate")]
        public string Validate (string id)
        {
            ////what is meant to return? ask away
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("validate")]
        public string ValidateJsonService (string id)
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