using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class UpgradeMembershipBusinessRule : IBusinessRule
    {
        public void ExecuteBusinessRule(Order order)
        {
            var membershipUpgrade = (MembershipUpgrade) order.Products.Find(product => product.GetType() == typeof(MembershipUpgrade));
            membershipUpgrade.Membership.IsUpgraded = true;
        }
    }
}
