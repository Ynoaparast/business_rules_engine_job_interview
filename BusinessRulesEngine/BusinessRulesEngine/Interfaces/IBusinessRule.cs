namespace BusinessRulesEngine.Interfaces
{
    public interface IBusinessRule
    {
        void ExecuteBusinessRule(IProduct product);
    }
}