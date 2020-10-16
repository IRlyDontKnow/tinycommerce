using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Events
{
    public class NewCustomerRegisteredDomainEvent : DomainEventBase
    {
        public NewCustomerRegisteredDomainEvent(
            CustomerRegistrationId customerRegistrationId,
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime registrationDate
        )
        {
            CustomerRegistrationId = customerRegistrationId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            RegistrationDate = registrationDate;
        }

        public CustomerRegistrationId CustomerRegistrationId { get; }
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime RegistrationDate { get; }
    }
}
