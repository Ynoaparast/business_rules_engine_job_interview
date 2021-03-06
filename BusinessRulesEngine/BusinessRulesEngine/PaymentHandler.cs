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
           
            if (payment.Order.Products.Exists(product => product.IsPhysical))
            {
              payment.BusinessRules.Add(new GeneratePackagingSlipBusinessRule("Customer"));
            }

            if (payment.Order.Products.Exists(product => product.GetType() == typeof(Book)))
            {
                payment.BusinessRules.Add(new GeneratePackagingSlipBusinessRule("Royalty"));
            }

            if (payment.Order.Products.Exists(product => product.IsPhysical) || payment.Order.Products.Exists(product => product.GetType() == typeof(Book)))
            {
                payment.BusinessRules.Add(new GenerateAgentCommissionBusinessRule());
            }

            if (payment.Order.Products.Exists(product => product.GetType() == typeof(Membership)))
            {
                payment.BusinessRules.Add(new ActivateMembershipBusinessRule());
                payment.BusinessRules.Add(new SendEmailForMembershipBusinessRule());
            }

            if (payment.Order.Products.Exists(product => product.GetType() == typeof(MembershipUpgrade)))
            {
                payment.BusinessRules.Add(new UpgradeMembershipBusinessRule());
                payment.BusinessRules.Add(new SendEmailForMembershipBusinessRule());
            }

            if (payment.Order.Products.Exists(product => product is Video { Title: "Learning to Ski" }))
            {
                payment.BusinessRules.Add(new AddFreeFirstAidVideoBusinessRule("Customer"));
            }

         
            return payment;
        }
    }
}
