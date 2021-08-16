using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class SendEmailForMembershipBusinessRule : IBusinessRule
    {
        public List<Email> Emails { get; set; }

        public SendEmailForMembershipBusinessRule()
        {
            Emails = new List<Email>();
        }
        public Email CreateEmailToSend(Product product, Customer customer)
        {
            
            var productText = product.GetType() == typeof(Membership) ? "membership has been activated" : "membership has been upgraded";
            var emailContent = $"Dear {customer.Firstname} {customer.Lastname}, your {productText}";
            var email = new Email()
            {
                Content = emailContent,
                To = customer.Email
            };

            return email;
        }

        public void ExecuteBusinessRule(Order order)
        {
            foreach (var product in order.Products)
            {
                if (product.GetType() == typeof(Membership) || product.GetType() == typeof(MembershipUpgrade))
                {
                    var email = CreateEmailToSend(product, order.Customer);
                    Emails.Add(email);
                }
            }
            
            //...Send Email(Emails)
        }
    }
}
