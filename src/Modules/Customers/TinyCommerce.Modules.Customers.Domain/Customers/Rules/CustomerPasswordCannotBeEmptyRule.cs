using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.Customers.Rules
{
    public class CustomerPasswordCannotBeEmptyRule : IBusinessRule
    {
        private readonly string _password;

        public CustomerPasswordCannotBeEmptyRule(string password)
        {
            _password = password;
        }

        public bool IsBroken()
        {
            return string.IsNullOrEmpty(_password);
        }

        public string Message => "Customer password cannot be empty rule.";
    }
}