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
    }
}
