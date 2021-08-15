using BusinessRulesEngine.Interfaces;

namespace BusinessRulesEngine.Models
{
    public class Product: IProduct
    {
        public bool IsPhysical { get; set; }
        public string ProductType { get; set; }
    }
}