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
              payment.BusinessRules.Add(new GeneratePackagingSlipBusinessRule());
            }

            return payment;
        }
    }
}
