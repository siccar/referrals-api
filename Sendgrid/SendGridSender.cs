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

        public SendGridSender(string apiKey, string templateId)
        {
            _apiKey = apiKey;
            _templateId = templateId;
        }

        public async Task SendSingleTemplateEmail(EmailAddress from, EmailAddress to)
        {
            var client = new SendGridClient(_apiKey);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, _templateId, null);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendSingleTemplateEmailToMultiple(EmailAddress from, List<EmailAddress> to)
        {
            var client = new SendGridClient(_apiKey);
            var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, to, _templateId, null);
            var response = await client.SendEmailAsync(msg);
        }
    }
}