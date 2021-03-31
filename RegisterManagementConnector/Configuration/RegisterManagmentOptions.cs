using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.Configuration
{
    public class RegisterManagmentOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantID { get; set; }
        public string BaseUrl { get; set; }
        public string ResourceUri { get; set; }
    }
}
