using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;

namespace BusinessRulesEngine.Models
{
    public class Membership : IProduct
    {
        public bool IsPhysical { get; set; }
        public string ProductType { get; set; }
        public bool IsActive { get; set; }
        public bool IsUpgraded { get; set; }
    }
}
