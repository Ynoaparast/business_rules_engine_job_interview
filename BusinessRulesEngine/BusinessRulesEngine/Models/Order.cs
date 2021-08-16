using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.BusinessRules;

namespace BusinessRulesEngine.Models
{
    public class Order
    {
        public Customer Customer { get; set; }
        public Agent Agent { get; set; }
        public List<PackagingSlip> PackagingSlips { get; set; }
        public List<Product> Products { get; set; }

        public Order()
        {
            PackagingSlips = new List<PackagingSlip>();
            Products = new List<Product>();
            Agent = new Agent();
        }
    }
}
