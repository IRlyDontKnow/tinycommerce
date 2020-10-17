using System;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Customers.Domain.Customers.Events;
using TinyCommerce.Modules.Customers.Domain.Customers.Rules;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;
using TinyCommerce.Modules.Customers.Domain.SeedWork;

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
            Email = email;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _registrationDate = registrationDate;

            AddDomainEvent(new CustomerCreatedDomainEvent(
                Id,
                Email,
                _password,
                _firstName,
                _lastName,
                _registrationDate
            ));
        }

        public CustomerId Id { get; }
        public string Email { get; }
        private string _password;
        private string _firstName;
        private string _lastName;
        private DateTime _registrationDate;

        public PasswordReminder RemindPassword(IResetCodeGenerator resetCodeGenerator)
        {
            return PasswordReminder.Create(
                new PasswordReminderId(Guid.NewGuid()),
                Email,
                resetCodeGenerator
            );
        }

        public void ChangePassword(string newPassword, Func<string, bool> verifyHashedPassword)
        {
            CheckRule(new CustomerPasswordCannotBeEmptyRule(newPassword));

            if (null != verifyHashedPassword)
                CheckRule(new ProvidedPasswordMustMatchCurrentOneRule(_password, verifyHashedPassword));

            var oldPassword = _password;
            _password = newPassword;

            AddDomainEvent(new CustomerPasswordChangedDomainEvent(
                Id,
                oldPassword,
                newPassword,
                SystemClock.Now
            ));
        }

        public void ResetPassword(PasswordReminder reminder, string newPassword)
        {
            CheckRule(new PasswordReminderCannotBeExpiredRule(reminder));
            ChangePassword(newPassword, null);

            AddDomainEvent(new CustomerPasswordResetDomainEvent(
                reminder.Id,
                Id,
                newPassword
            ));
        }

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