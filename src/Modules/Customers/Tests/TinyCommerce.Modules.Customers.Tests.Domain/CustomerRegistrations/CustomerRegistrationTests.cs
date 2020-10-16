using NSubstitute;
using NUnit.Framework;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Events;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Rules;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Tests.Domain.SeedWork;

namespace TinyCommerce.Modules.Customers.Tests.Domain.CustomerRegistrations
{
    [TestFixture]
    public class CustomerRegistrationTests : TestBase
    {
        [Test]
        public void RegisterNewCustomer_WithValidData_ShouldSucceed()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            customerChecker.IsCustomerEmailInUse(CustomerRegistrationSampleData.Email)
                .Returns(false);

            var registration = CustomerRegistration.RegisterNewCustomer(
                CustomerRegistrationSampleData.Id,
                CustomerRegistrationSampleData.Email,
                CustomerRegistrationSampleData.Password,
                CustomerRegistrationSampleData.FirstName,
                CustomerRegistrationSampleData.LastName,
                customerChecker
            );

            Assert.That(registration, Is.Not.Null);

            var domainEvent = AssertPublishedDomainEvent<NewCustomerRegisteredDomainEvent>(registration);

            Assert.That(domainEvent.CustomerRegistrationId, Is.EqualTo(CustomerRegistrationSampleData.Id));
            Assert.That(domainEvent.Email, Is.EqualTo(CustomerRegistrationSampleData.Email));
            Assert.That(domainEvent.Password, Is.EqualTo(CustomerRegistrationSampleData.Password));
            Assert.That(domainEvent.FirstName, Is.EqualTo(CustomerRegistrationSampleData.FirstName));
            Assert.That(domainEvent.LastName, Is.EqualTo(CustomerRegistrationSampleData.LastName));
        }

        [Test]
        public void RegisterNewCustomer_WithAlreadyUsedEmail_ShouldFail()
        {
            var customerChecker = Substitute.For<ICustomerChecker>();
            customerChecker.IsCustomerEmailInUse(CustomerRegistrationSampleData.Email)
                .Returns(true);

            AssertBrokenRule<CustomerEmailMustBeUniqueRule>(() =>
            {
                CustomerRegistration.RegisterNewCustomer(
                    CustomerRegistrationSampleData.Id,
                    CustomerRegistrationSampleData.Email,
                    CustomerRegistrationSampleData.Password,
                    CustomerRegistrationSampleData.FirstName,
                    CustomerRegistrationSampleData.LastName,
                    customerChecker
                );
            });
        }
    }
}