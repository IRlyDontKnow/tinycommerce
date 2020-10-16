using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.Customers.Events
{
    public class CustomerCreatedDomainEvent : DomainEventBase
    {
        public CustomerCreatedDomainEvent(
            CustomerId customerId,
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime registrationDate
        )
        {
            CustomerId = customerId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            RegistrationDate = registrationDate;
        }

        public CustomerId CustomerId { get; }
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime RegistrationDate { get; }
    }
}