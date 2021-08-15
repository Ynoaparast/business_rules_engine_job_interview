using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;

namespace BusinessRulesEngine.BusinessRules
{
    public class GeneratePackagingSlipBusinessRule : IBusinessRule
    {
        public string SlipDestination { get; set; }
        public void ExecuteBusinessRule(IProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
