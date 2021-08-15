using System.Collections.Generic;
using BusinessRulesEngine.Interfaces;

namespace BusinessRulesEngine.Models
{
    public class Payment: IPayment
    {
        public Product Product { get; set; }
        public List<IBusinessRule> BusinessRules { get; set; } = new List<IBusinessRule>();

        public void ExecuteBusinessRules()
        {
            foreach (var rule in BusinessRules)
            {
                rule.ExecuteBusinessRule(Product);
            };
        }
    }
}