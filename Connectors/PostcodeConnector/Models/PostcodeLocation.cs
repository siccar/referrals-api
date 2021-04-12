using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.PostcodeConnector.Models
{

    public class PostcodeLocation
    {
        public string Postcode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class PostcodeResult
    {
        public int Status { get; set; }
        public PostcodeLocation Result {get;set;}
}
}
