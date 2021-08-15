﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.Interfaces
{
    public interface IPayment
    {
        public Product Product { get; set; }
        List<IBusinessRule> BusinessRules { get; set; }
        public void ExecuteBusinessRules();
    }
}
