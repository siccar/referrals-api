using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
        public Service Service { get; set; }
        public string Title { get; set; }
     }
}
