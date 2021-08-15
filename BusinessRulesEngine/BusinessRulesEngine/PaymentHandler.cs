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

            if (payment.Product.ProductType == "Book")
            {
                payment.BusinessRules.Add(new GeneratePackagingSlipBusinessRule(){SlipDestination = "Royalty"});
            }

            if (payment.Product.IsPhysical || payment.Product.ProductType == "Book")
            {
                payment.BusinessRules.Add(new GenerateAgentCommissionBusinessRule());
            }

            return payment;
        }
    }
}
