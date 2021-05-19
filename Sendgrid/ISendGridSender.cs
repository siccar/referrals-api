using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferrals.Sendgrid
{
    public interface ISendGridSender
    {
        Task SendSingleTemplateEmail(EmailAddress from, EmailAddress to, string orgName);
        Task SendOrgRequestEmail(EmailAddress from, EmailAddress to, string orgName);
        Task SendOrgApprovedEmail(EmailAddress from, EmailAddress to, string orgName, string orgId);
    }
}