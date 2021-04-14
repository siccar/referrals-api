﻿using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Spatial;
using System.Threading.Tasks;

namespace OpenReferrals.Connectors.LocationSearchConnector.Models
{
    public class SearchLocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public GeographyPoint Point { get; set; }
        public IEnumerable<PhysicalAddress> Physical_Addresses { get; set; }
    }
}
