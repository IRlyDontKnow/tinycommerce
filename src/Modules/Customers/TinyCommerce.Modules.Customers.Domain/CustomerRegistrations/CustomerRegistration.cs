using System;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Events;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Rules;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Domain.SeedWork;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations
{
    public class CustomerRegistration : Entity
    {
        private CustomerRegistration()
        {
            // Entity framework
        }

        private CustomerRegistration(
            CustomerRegistrationId id,
            string email,
            string password,
            string firstName,
            string lastName,
            ICustomerChecker customerChecker
        )
        {
            CheckRule(new CustomerEmailMustBeUniqueRule(email, customerChecker));

            Id = id;
            _email = email;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _status = CustomerRegistrationStatus.WaitingForConfirmation;
            _registrationDate = SystemClock.Now;

            AddDomainEvent(new NewCustomerRegisteredDomainEvent(
                Id,
                _email,
                _password,
                _firstName,
                _lastName,
                _registrationDate
            ));
        }

        public CustomerRegistrationId Id { get; }
        private string _email;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _activationCode;
        private CustomerRegistrationStatus _status;
        private DateTime _registrationDate;

        public static CustomerRegistration RegisterNewCustomer(
            CustomerRegistrationId id,
            string email,
            string password,
            string firstName,
            string lastName,
            ICustomerChecker customerChecker
        )
        {
            return new CustomerRegistration(id, email, password, firstName, lastName, customerChecker);
        }

        public void Confirm(string providedActivationCode)
        {
            if (!string.IsNullOrEmpty(providedActivationCode))
            {
                CheckRule(new ProvidedActivationCodeMustMatchActualOneRule(
                    providedActivationCode,
                    _activationCode
                ));
            }

            CheckRule(new CustomerRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
            CheckRule(new CannotConfirmExpiredCustomerRegistrationRule(_status));

            _status = CustomerRegistrationStatus.Confirmed;

            AddDomainEvent(new CustomerRegistrationConfirmedDomainEvent(Id));
        }

        public Customer CreateCustomer(ICustomerChecker customerChecker)
        {
            return Customer.CreateNew(
                new CustomerId(Id.Value),
                _email,
                _password,
                _firstName,
                _lastName,
                _registrationDate,
                customerChecker
            );
        }
    }
}