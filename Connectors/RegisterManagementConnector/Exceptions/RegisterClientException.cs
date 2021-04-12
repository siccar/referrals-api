using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.Exceptions
{
    public class RegisterClientException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public RegisterClientException(HttpStatusCode status, string message) : base(message)
        {
            Status = status;
        }
    }
}
