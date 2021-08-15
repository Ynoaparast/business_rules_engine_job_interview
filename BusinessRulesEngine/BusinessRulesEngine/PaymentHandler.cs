using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.BusinessRules;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine
{
    public class PaymentHandler
    {

        public PaymentHandler()
        {

        }

        public Payment ApplyBusinessRules(Payment payment)
        {
           
            if (payment.Product.IsPhysical)
            {
              payment.BusinessRules.Add(new GeneratePackagingSlipBusinessRule(){SlipDestination = "Customer"});
            }

            if (payment.Product.GetType() == typeof(Book))
            {
                payment.BusinessRules.Add(new GeneratePackagingSlipBusinessRule(){SlipDestination = "Royalty"});
            }

            if (payment.Product.IsPhysical || payment.Product.GetType() == typeof(Book))
            {
                payment.BusinessRules.Add(new GenerateAgentCommissionBusinessRule());
            }

            if (payment.Product.GetType() == typeof(Membership))
            {
                payment.BusinessRules.Add(new ActivateMembershipBusinessRule());
            }

            if (payment.Product.GetType() == typeof(MembershipUpgrade))
            {
                payment.BusinessRules.Add(new UpgradeMembershipBusinessRule());
            }
            return payment;
        }
    }
}
