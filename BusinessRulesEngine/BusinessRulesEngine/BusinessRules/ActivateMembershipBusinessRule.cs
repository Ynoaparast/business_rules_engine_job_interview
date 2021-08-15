using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class ActivateMembershipBusinessRule : IBusinessRule
    {
        public void ExecuteBusinessRule(IProduct product)
        {
            var membershipProduct = (Membership) product;
            membershipProduct.IsActive = true;
        }
    }
}
