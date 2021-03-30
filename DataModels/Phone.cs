using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Phone
    {
        public Contact Contact { get; set; }
        public string Id { get; set; }
        public string Number { get; set; }
    }
}
