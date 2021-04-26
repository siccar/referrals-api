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
        private string _templateId;
        private string _baseAddress;
        private IHttpContextAccessor _httpContextAccessor; 

        public SendGridSender(SendGridSettings sendGridSettings, IHttpContextAccessor httpContextAccessor)
        {
            _apiKey = sendGridSettings.ApiKey;
            _templateId = sendGridSettings.TemplateId;
            _baseAddress = sendGridSettings.BaseAddress;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendSingleTemplateEmail(EmailAddress from, EmailAddress to, string orgName)
        {

            var templateParameters = new TemplateParameters
            {
                OpenReferralAppUrl = _baseAddress + "myrequests",
                OrganisationName = orgName,
                UserName = JWTAttributesService.GetEmail(_httpContextAccessor.HttpContext.Request),
        };
            var client = new SendGridClient(_apiKey);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, _templateId, templateParameters);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendSingleTemplateEmailToMultiple(EmailAddress from, List<EmailAddress> to)
        {
            var templateParameters = new TemplateParameters
            {
                
                OpenReferralAppUrl = _baseAddress + "myrequests",
            };
            var client = new SendGridClient(_apiKey);
            var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, to, _templateId, templateParameters);
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