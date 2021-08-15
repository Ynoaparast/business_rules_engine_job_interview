using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class SendEmailForMembershipBusinessRule : IBusinessRule
    {
        public Email Email { get; set; }
        public Customer Customer { get; set; }

        public SendEmailForMembershipBusinessRule(Customer customer)
        {
            Customer = customer;
        }

        public void CreateEmailToSend(Product product)
        {
            var productText = product.GetType() == typeof(Membership) ? "membership has been activated" : "membership has been upgraded";
            var emailContent = $"Dear {Customer.Firstname} {Customer.Lastname}, your {productText}";
            Email = new Email()
            {
                Content = emailContent,
                To = Customer.Email
            };
   

        }

        public void ExecuteBusinessRule(Product product)
        {
            CreateEmailToSend(product);

            //...Send Email
        }
    }
}
