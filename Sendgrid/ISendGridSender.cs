﻿using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferrals.Sendgrid
{
    public interface ISendGridSender
    {
        Task SendSingleTemplateEmail(EmailAddress from, EmailAddress to, string orgName);
        Task SendSingleTemplateEmailToMultiple(EmailAddress from, List<EmailAddress> to);
    }
}