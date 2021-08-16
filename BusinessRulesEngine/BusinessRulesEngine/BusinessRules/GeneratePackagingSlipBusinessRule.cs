using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class GeneratePackagingSlipBusinessRule : IBusinessRule
    {

        public string SlipDestination { get; set; }

        public GeneratePackagingSlipBusinessRule(string slipDestination)
        {
            SlipDestination = slipDestination;
        }

        public void ExecuteBusinessRule(Order order)
        {
            var packagingSlip = new PackagingSlip(SlipDestination);

            foreach (var product in order.Products)
            {
                packagingSlip.ProductsToPack.Add(product);
            }

            order.PackagingSlips.Add(new PackagingSlip(SlipDestination));

        }
    }
}
