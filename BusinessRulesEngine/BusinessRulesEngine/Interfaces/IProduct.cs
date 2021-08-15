using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Interfaces
{
    public interface IProduct
    {
        public bool IsPhysical { get; set; }
        public string ProductType { get; set; }
    }
}
