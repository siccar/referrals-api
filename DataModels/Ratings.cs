using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    public class Ratings
    {
        public RatingSummary ratings { get; set; }
        public double Richness_Percentage { get; set; }
        public int Sample_Size { get; set; }
    }
}
