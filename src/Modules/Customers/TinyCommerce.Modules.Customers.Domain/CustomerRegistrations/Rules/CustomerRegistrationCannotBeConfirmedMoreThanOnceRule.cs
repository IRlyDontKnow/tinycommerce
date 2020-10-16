using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Rules
{
    public class CustomerRegistrationCannotBeConfirmedMoreThanOnceRule : IBusinessRule
    {
        private readonly CustomerRegistrationStatus _status;

        public CustomerRegistrationCannotBeConfirmedMoreThanOnceRule(CustomerRegistrationStatus status)
        {
            _status = status;
        }

        public bool IsBroken()
        {
            return _status == CustomerRegistrationStatus.Confirmed;
        }

        public string Message => "Customer registration cannot be confirmed more than once.";
    }
}