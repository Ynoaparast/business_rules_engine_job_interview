using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.Interfaces
{
    public interface IBusinessRule
    {
        void ExecuteBusinessRule(Product product);
    }
}