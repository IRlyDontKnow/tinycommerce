using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.Customers.Rules
{
    public class CustomerEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly string _email;
        private readonly ICustomerChecker _checker;

        public CustomerEmailMustBeUniqueRule(string email, ICustomerChecker checker)
        {
            _email = email;
            _checker = checker;
        }

        public bool IsBroken()
        {
            return _checker.IsCustomerEmailInUse(_email);
        }

        public string Message => "Customer e-mail must be unique.";
    }
}