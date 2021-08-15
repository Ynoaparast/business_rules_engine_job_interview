using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class UpgradeMembershipBusinessRule : IBusinessRule
    {
        public void ExecuteBusinessRule(Product product)
        {
            var membership = (Membership) product;
            membership.IsUpgraded = true;
        }
    }
}
