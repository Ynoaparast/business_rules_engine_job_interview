using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.Interfaces
{
    public interface IBusinessRule
    {
        void ExecuteBusinessRule(Order order);
    }
}