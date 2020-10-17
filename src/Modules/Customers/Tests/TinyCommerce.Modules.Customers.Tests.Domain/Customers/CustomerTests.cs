using System;
using NSubstitute;
using NUnit.Framework;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Domain.Customers.Events;
using TinyCommerce.Modules.Customers.Domain.Customers.Rules;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders.Events;
using TinyCommerce.Modules.Customers.Domain.SeedWork;
using TinyCommerce.Modules.Customers.Tests.Domain.SeedWork;

namespace TinyCommerce.Modules.Customers.Tests.Domain.Customers
{
    [TestFixture]
    public class CustomerTests : TestBase
    {
        [Test]
        public void CreateNew_Test()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            customerChecker.IsCustomerEmailInUse(CustomerSampleData.Email).Returns(false);

            var customer = Customer.CreateNew(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName,
                CustomerSampleData.RegistrationDate,
                customerChecker
            );

            var domainEvent = AssertPublishedDomainEvent<CustomerCreatedDomainEvent>(customer);

            Assert.That(domainEvent.CustomerId, Is.EqualTo(CustomerSampleData.Id));
            Assert.That(domainEvent.Email, Is.EqualTo(CustomerSampleData.Email));
            Assert.That(domainEvent.Password, Is.EqualTo(CustomerSampleData.Password));
            Assert.That(domainEvent.FirstName, Is.EqualTo(CustomerSampleData.FirstName));
            Assert.That(domainEvent.LastName, Is.EqualTo(CustomerSampleData.LastName));
            Assert.That(domainEvent.RegistrationDate, Is.EqualTo(CustomerSampleData.RegistrationDate));
        }

        [Test]
        public void TryCreateNew_WithAlreadyUsedEmail_Test()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            customerChecker.IsCustomerEmailInUse(CustomerSampleData.Email).Returns(true);

            AssertBrokenRule<CustomerEmailMustBeUniqueRule>(() =>
            {
                Customer.CreateNew(
                    CustomerSampleData.Id,
                    CustomerSampleData.Email,
                    CustomerSampleData.Password,
                    CustomerSampleData.FirstName,
                    CustomerSampleData.LastName,
                    CustomerSampleData.RegistrationDate,
                    customerChecker
                );
            });
        }

        [Test]
        public void ChangePassword_Test()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            var customer = Customer.CreateNew(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName,
                CustomerSampleData.RegistrationDate,
                customerChecker
            );

            customer.ChangePassword("newPass", s => true);

            var domainEvent = AssertPublishedDomainEvent<CustomerPasswordChangedDomainEvent>(customer);

            Assert.That(domainEvent.CustomerId, Is.EqualTo(CustomerSampleData.Id));
            Assert.That(domainEvent.OldPassword, Is.EqualTo(CustomerSampleData.Password));
            Assert.That(domainEvent.NewPassword, Is.EqualTo("newPass"));
        }

        [Test]
        public void TryChangePassword_WithInvalidCurrentPassword_Test()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            var customer = Customer.CreateNew(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName,
                CustomerSampleData.RegistrationDate,
                customerChecker
            );

            AssertBrokenRule<ProvidedPasswordMustMatchCurrentOneRule>(() =>
            {
                customer.ChangePassword("newPass", s => false);
            });
        }

        [Test]
        public void TryChangePassword_WithEmptyNewPassword_Test()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            var customer = Customer.CreateNew(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName,
                CustomerSampleData.RegistrationDate,
                customerChecker
            );

            AssertBrokenRule<CustomerPasswordCannotBeEmptyRule>(() => { customer.ChangePassword(null, s => true); });
        }

        [Test]
        public void RemindPassword_Test()
        {
            var resetCodeGenerator = Substitute.For<IResetCodeGenerator>();
            resetCodeGenerator.Generate().Returns("KQO2B7");

            var customer = Customer.CreateNew(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName,
                CustomerSampleData.RegistrationDate,
                Substitute.For<ICustomerChecker>()
            );

            var passwordReminder = customer.RemindPassword(resetCodeGenerator);

            var domainEvent = AssertPublishedDomainEvent<PasswordReminderCreatedDomainEvent>(passwordReminder);

            Assert.That(domainEvent.Code, Is.EqualTo("KQO2B7"));
            Assert.That(domainEvent.Email, Is.EqualTo(CustomerSampleData.Email));
        }

        [Test]
        public void ResetPassword_Test()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            var resetCodeGenerator = Substitute.For<IResetCodeGenerator>();

            var customer = Customer.CreateNew(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName,
                CustomerSampleData.RegistrationDate,
                customerChecker
            );

            var passwordReminder = customer.RemindPassword(resetCodeGenerator);

            customer.ResetPassword(passwordReminder, "NewPassword123");

            var domainEvent = AssertPublishedDomainEvent<CustomerPasswordResetDomainEvent>(customer);

            Assert.That(domainEvent.PasswordReminderId, Is.EqualTo(passwordReminder.Id));
            Assert.That(domainEvent.NewPassword, Is.EqualTo("NewPassword123"));
        }

        [Test]
        public void TryResetPassword_WithExpiredPasswordReminder_ShouldFail()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            var resetCodeGenerator = Substitute.For<IResetCodeGenerator>();

            var customer = Customer.CreateNew(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName,
                CustomerSampleData.RegistrationDate,
                customerChecker
            );

            SystemClock.Set(DateTime.UtcNow.AddDays(-5));

            var passwordReminder = customer.RemindPassword(resetCodeGenerator);

            SystemClock.Reset();

            AssertBrokenRule<PasswordReminderCannotBeExpiredRule>(() =>
            {
                customer.ResetPassword(passwordReminder, "NewPassword123");
            });
        }
    }
}