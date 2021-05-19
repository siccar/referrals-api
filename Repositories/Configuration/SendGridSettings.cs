using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.Configuration
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; }
        public string TemplateId{ get; set; }
        public string OrgPendingTemplate { get; set; }
        public string OrgApprovedTemplate { get; set; }
        public string BaseAddress { get; set; }
    }
}
