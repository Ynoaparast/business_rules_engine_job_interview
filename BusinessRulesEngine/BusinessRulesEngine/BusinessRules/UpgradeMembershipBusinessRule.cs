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
            var membershipUpgrade =  (MembershipUpgrade) product;
            membershipUpgrade.Membership.IsUpgraded = true;
        }
    }
}
