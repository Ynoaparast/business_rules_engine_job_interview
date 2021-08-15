using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;

namespace BusinessRulesEngine.Models
{
    public class Membership : Product
    {
        public bool IsActive { get; set; }
        public bool IsUpgraded { get; set; }
    }
}
