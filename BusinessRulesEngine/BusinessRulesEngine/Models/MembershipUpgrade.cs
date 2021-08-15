using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;

namespace BusinessRulesEngine.Models
{
    public class MembershipUpgrade : Product
    {
        public Membership Membership { get; set; }
    }
}
