using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class PhysicalAddress
    {
        public string Address_1 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Id { get; set; }
        public string LocationId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postcode must be populated")]
        [RegularExpression(@"(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})",
 ErrorMessage = "The Postcode must be a valid UK Postcode.")]
        public string Postal_Code { get; set; }
        public string State_Province { get; set; }
    }
}
