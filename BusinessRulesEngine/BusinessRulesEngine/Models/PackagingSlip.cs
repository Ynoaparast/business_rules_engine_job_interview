using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Models
{
    public class PackagingSlip
    {
        public List<Product> ProductsToPack { get; set; }
        public string SlipDestination { get; set; }

        public PackagingSlip(string slipDestination)
        {
            SlipDestination = slipDestination;
            ProductsToPack = new List<Product>();
        }

    }
}
