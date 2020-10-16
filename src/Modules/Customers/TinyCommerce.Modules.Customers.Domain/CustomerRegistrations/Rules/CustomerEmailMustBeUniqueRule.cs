using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Customers.Domain.Customers;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Rules
{
    public class CustomerEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly string _email;
        private readonly ICustomerChecker _customerChecker;

        public CustomerEmailMustBeUniqueRule(string email, ICustomerChecker customerChecker)
        {
            _email = email;
            _customerChecker = customerChecker;
        }

        public bool IsBroken()
        {
            return _customerChecker.IsCustomerEmailInUse(_email);
        }

        public string Message => "Customer e-mail address must be unique.";
    }
}
