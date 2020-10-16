using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Rules
{
    public class CannotConfirmExpiredCustomerRegistrationRule : IBusinessRule
    {
        private readonly CustomerRegistrationStatus _status;

        public CannotConfirmExpiredCustomerRegistrationRule(CustomerRegistrationStatus status)
        {
            _status = status;
        }

        public bool IsBroken()
        {
            return _status == CustomerRegistrationStatus.Expired;
        }

        public string Message => "Cannot confirmed expired customer registration.";
    }
}