using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class ServiceAtLocation
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Location_Id { get; set; }
        [Required]
        public string Service_Id { get; set; }
    }
}
