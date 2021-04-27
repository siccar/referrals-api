using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenReferrals.DataModels;
using OpenReferrals.Policies;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using OpenReferrals.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganisationRepository _orgRepository;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;
        private readonly IKeyContactRepository _keyContactRepo;
        private readonly IOrganisationMemberRepository _orgMemberRepo;
        private readonly IAuthorizationService _authorizationService;

        public OrganizationsController(
            IOrganisationRepository orgRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient,
            IKeyContactRepository keyContactRepository,
            IOrganisationMemberRepository orgMemberRepo,
            IAuthorizationService authorizationService
            )
        {
            _orgRepository = orgRepository;
            _registerManagmentServiceClient = registerManagmentServiceClient;
            _keyContactRepo = keyContactRepository;
            _orgMemberRepo = orgMemberRepo;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Get All Services
        /// </summary>
        /// <param name="text">The postcode of the person who wishes to use the service. In order to find services that are within a reasonable distance.</param>
        /// <returns>A <see cref="List{Organisation}"/>Returns all services based on input parameters</returns>
        [HttpGet]
        public IActionResult Get(string text = null)
        {
            var orgs = _orgRepository.GetAll();

            if (text != null)
            {
                var filteredList = orgs.Where(org => org.Name.ToLower().Contains(text.ToLower())).ToList();
                return Ok(filteredList);
            }

            return Ok(orgs);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Organisation organisation, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            var currentOrgs = _orgRepository.GetAll();
            try
            {
                Guid.Parse(organisation.Id);
            }
            catch (Exception _)
            {
                ModelState.AddModelError(nameof(Organisation.Id), "Organization Id is not a valid Guid");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            if (currentOrgs.Any(org => org.CharityNumber == organisation.CharityNumber))
            {
                ModelState.AddModelError(nameof(Organisation.CharityNumber), "Charity number already exists.");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            if (currentOrgs.Any(org => org.Name == organisation.Name))
            {
                ModelState.AddModelError(nameof(Organisation.Name), "Organisation name already exists.");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            else
            {
                var publishedOrg = _registerManagmentServiceClient.CreateOrganisation(organisation);
                await _orgRepository.InsertOne(publishedOrg);
                return Accepted(publishedOrg);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var userId = JWTAttributesService.GetSubject(Request);
            var org = await _orgRepository.FindById(id);
            var keyContactList = await _keyContactRepo.FindByOrgId(id);

            return Ok(org);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] Organisation organisation)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, AuthzPolicyNames.MustBeOrgAdmin);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            //This does nothing when SiccarConnect flag is false
            _registerManagmentServiceClient.UpdateOrganisation(organisation);

            await _orgRepository.UpdateOne(organisation);
            return Ok();
        }
    }
}