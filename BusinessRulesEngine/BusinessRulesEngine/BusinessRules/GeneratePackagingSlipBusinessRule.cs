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
        public void ExecuteBusinessRule(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
