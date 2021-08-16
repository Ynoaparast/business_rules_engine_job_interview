using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class GenerateAgentCommissionBusinessRule : IBusinessRule
    {
        public void ExecuteBusinessRule(Order order)
        {
            var agent = order.Agent;
            agent.Commission = 100;
        }
    }
}
