using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class ActivateMembershipBusinessRule : IBusinessRule
    {
        public void ExecuteBusinessRule(Order order)
        {
            var product = (Membership)order.Products.Find(product => product.GetType() == typeof(Membership));
            var membershipProduct = product;
            membershipProduct.IsActive = true;
        }
    }
}
