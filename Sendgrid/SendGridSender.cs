using Microsoft.AspNetCore.Http;
using OpenReferrals.Repositories.Configuration;
using OpenReferrals.Sevices;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Sendgrid
{
    public class SendGridSender : ISendGridSender
    {
        private string _apiKey;
        private SendGridSettings _settings;
        private IHttpContextAccessor _httpContextAccessor;

        public SendGridSender(SendGridSettings sendGridSettings, IHttpContextAccessor httpContextAccessor)
        {
            _apiKey = sendGridSettings.ApiKey;
            _settings = sendGridSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendSingleTemplateEmail(EmailAddress from, EmailAddress to, string orgName)
        {

            var templateParameters = new TemplateParameters
            {
                OpenReferralAppUrl = _settings.BaseAddress + "myrequests",
                OrganisationName = orgName,
                UserName = JWTAttributesService.GetEmail(_httpContextAccessor.HttpContext.Request),
            };
            var client = new SendGridClient(_apiKey);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, _settings.TemplateId, templateParameters);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendOrgRequestEmail(EmailAddress from, EmailAddress to, string orgName)
        {

            var templateParameters = new TemplateParameters
            {
                OpenReferralAppUrl = _settings.BaseAddress + "myrequests",
                OrganisationName = orgName,
                UserName = JWTAttributesService.GetEmail(_httpContextAccessor.HttpContext.Request),
            };
            var client = new SendGridClient(_apiKey);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, _settings.OrgPendingTemplate, templateParameters);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendOrgApprovedEmail(EmailAddress from, EmailAddress to, string orgName, string orgId)
        {

            var templateParameters = new TemplateParameters
            {
                OpenReferralAppUrl = _settings.BaseAddress + $"manage-organisation/{orgId}",
                OrganisationName = orgName,
                UserName = JWTAttributesService.GetEmail(_httpContextAccessor.HttpContext.Request),
            };
            var client = new SendGridClient(_apiKey);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, _settings.OrgApprovedTemplate, templateParameters);
            var response = await client.SendEmailAsync(msg);
        }
    }

    public class TemplateParameters
    {
        public string OpenReferralAppUrl { get; set; }
        public string UserName { get; set; }
        public string OrganisationName { get; set; }
    }
}