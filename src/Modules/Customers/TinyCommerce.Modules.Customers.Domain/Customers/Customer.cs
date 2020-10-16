using System;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Rules;
using TinyCommerce.Modules.Customers.Domain.Customers.Events;

namespace TinyCommerce.Modules.Customers.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        private Customer()
        {
            // EF Core
        }

        private Customer(
            CustomerId id,
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime registrationDate,
            ICustomerChecker customerChecker
        )
        {
            CheckRule(new CustomerEmailMustBeUniqueRule(email, customerChecker));

            Id = id;
            _email = email;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _registrationDate = registrationDate;

            AddDomainEvent(new CustomerCreatedDomainEvent(
                id,
                email,
                password,
                firstName,
                lastName,
                registrationDate
            ));
        }

        public CustomerId Id { get; }
        private string _email;
        private string _password;
        private string _firstName;
        private string _lastName;
        private DateTime _registrationDate;

        public static Customer CreateNew(
            CustomerId id,
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime registrationDate,
            ICustomerChecker customerChecker
        )
        {
            return new Customer(id, email, password, firstName, lastName, registrationDate, customerChecker);
        }
    }
}