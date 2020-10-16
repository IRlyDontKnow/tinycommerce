using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Events
{
    public class CustomerRegistrationConfirmedDomainEvent : DomainEventBase
    {
        public CustomerRegistrationConfirmedDomainEvent(CustomerRegistrationId customerRegistrationId)
        {
            CustomerRegistrationId = customerRegistrationId;
        }

        public CustomerRegistrationId CustomerRegistrationId { get; }
    }
}