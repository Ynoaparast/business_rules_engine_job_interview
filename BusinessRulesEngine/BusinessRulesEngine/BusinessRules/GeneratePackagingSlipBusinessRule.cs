using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class GeneratePackagingSlipBusinessRule : IBusinessRule
    {

        public PackagingSlip PackagingSlip;

        public GeneratePackagingSlipBusinessRule(string slipDestination)
        {
            PackagingSlip = new PackagingSlip(slipDestination);
        }

        public void ExecuteBusinessRule(Product product)
        {
            PackagingSlip.ProductsToPack.Add(product);
        }
    }
}
