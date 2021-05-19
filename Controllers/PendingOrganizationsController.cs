using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using OpenReferrals.Repositories.OpenReferral.PendingOrgs;
using OpenReferrals.Sendgrid;
using OpenReferrals.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PendingOrganizationsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrganisationRepository _orgRepository;
        private readonly IPendingOrganisationRepository _pendingOrgRepository;
        private readonly IKeyContactRepository _keyContactRepo;
        private readonly IOrganisationMemberRepository _orgMemberRepo;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISendGridSender _sendgridSender;
        private readonly IRegisterManagmentServiceClient _registerManagmentServiceClient;

        public PendingOrganizationsController(
            IConfiguration configuration,
            IOrganisationRepository orgRepository,
            IPendingOrganisationRepository pendingOrgRepository,
            IRegisterManagmentServiceClient registerManagmentServiceClient,
            IKeyContactRepository keyContactRepository,
            IOrganisationMemberRepository orgMemberRepo,
            IAuthorizationService authorizationService,
            ISendGridSender sendGrideSender
            )
        {
            _configuration = configuration;
            _orgRepository = orgRepository;
            _pendingOrgRepository = pendingOrgRepository;
            _keyContactRepo = keyContactRepository;
            _orgMemberRepo = orgMemberRepo;
            _authorizationService = authorizationService;
            _sendgridSender = sendGrideSender;
            _registerManagmentServiceClient = registerManagmentServiceClient;
        }

        [HttpGet]
        public IActionResult Get(string text = null)
        {

            if (JWTAttributesService.GetSubject(Request) != _configuration["Admin:UserId"])
            {
                return Forbid();
            }

            var orgs = _pendingOrgRepository.GetAll();

            if (text != null)
            {
                var filteredList = orgs.Where(org => org.Name.ToLower().Contains(text.ToLower())).ToList();
                return Ok(filteredList);
            }

            return Ok(orgs);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PendingOrganisation organisation, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            var currentOrgs = _orgRepository.GetAll();
            var pendingOrgs = _pendingOrgRepository.GetAll();
            try
            {
                Guid.Parse(organisation.Id);
            }
            catch (Exception _)
            {
                ModelState.AddModelError(nameof(Organisation.Id), "Organization Id is not a valid Guid");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            if (organisation.CharityNumber > 0
                && (currentOrgs.Any(org => org.CharityNumber == organisation.CharityNumber)
                || pendingOrgs.Any(org => org.CharityNumber == organisation.CharityNumber)))
            {
                ModelState.AddModelError(nameof(Organisation.CharityNumber), "Charity number already exists.");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            if (currentOrgs.Any(org => org.Name == organisation.Name) || pendingOrgs.Any(org => org.Name == organisation.Name))
            {
                ModelState.AddModelError(nameof(Organisation.Name), "Organisation name already exists.");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            else
            {
                organisation.UserId = JWTAttributesService.GetSubject(Request);
                organisation.UserEmail = JWTAttributesService.GetEmail(Request);
                await _pendingOrgRepository.InsertOne(organisation);

                await _sendgridSender.SendOrgRequestEmail(
                    new SendGrid.Helpers.Mail.EmailAddress("support@wearecast.org.uk"),
                    new SendGrid.Helpers.Mail.EmailAddress(_configuration["Admin:EmailAddress"]),
                    organisation.Name
                    );

                return Accepted(organisation);
            }
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> VerifyPendingOrganisation(string id)
        {
            var org = await _pendingOrgRepository.FindById(id);

            if (JWTAttributesService.GetSubject(Request) != _configuration["Admin:UserId"])
            {
                return Forbid();
            }
            Organisation newOrg = new Organisation
            {
                Id = org.Id,
                Description = org.Description,
                CharityNumber = org.CharityNumber,
                Name = org.Name,
                ServicesIds = org.ServicesIds,
                Url = org.Url
            };

            await _orgRepository.InsertOne(newOrg);
            await _keyContactRepo.InsertOne(new KeyContacts() { Id = Guid.NewGuid().ToString(), OrgId = org.Id, UserId = org.UserId, UserEmail = org.UserEmail });
            await _pendingOrgRepository.DeleteOne(org);
            var publishedOrg = _registerManagmentServiceClient.CreateOrganisation(org);


            await _sendgridSender.SendOrgApprovedEmail(
                new SendGrid.Helpers.Mail.EmailAddress("support@wearecast.org.uk"),
                new SendGrid.Helpers.Mail.EmailAddress(org.UserEmail),
                org.Name,
                org.Id
                );

            return Accepted(publishedOrg);
        }

        [HttpGet]
        [Route("has-pending-org")]
        public IActionResult UserHasPendingOrg()
        {
            var orgs = _pendingOrgRepository.GetAll();

            var userHasPendingOrgs = orgs.Any(org => org.UserId == JWTAttributesService.GetSubject(Request));
            return Ok(userHasPendingOrgs);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (JWTAttributesService.GetSubject(Request) != _configuration["Admin:UserId"])
            {
                return Forbid();
            }

            var org = await _pendingOrgRepository.FindById(id);
            await _pendingOrgRepository.DeleteOne(org);

            return Ok();
        }
    }
}
