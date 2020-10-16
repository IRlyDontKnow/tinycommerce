using System.Threading.Tasks;
using NUnit.Framework;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.ConfirmRegistration;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.GetCustomerRegistration;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.RegisterNewCustomer;
using TinyCommerce.Modules.Customers.Application.Customers.CreateCustomer;
using TinyCommerce.Modules.Customers.Application.Customers.GetCustomer;
using TinyCommerce.Modules.Customers.Tests.Integration.SeedWork;

namespace TinyCommerce.Modules.Customers.Tests.Integration.Customers
{
    [TestFixture]
    public class CustomerTests : TestBase
    {
        [Test]
        public async Task CreateCustomerTest()
        {
            await CustomersModule.ExecuteCommandAsync(new CreateCustomerCommand(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName
            ));

            var customer = await CustomersModule.ExecuteQueryAsync(
                new GetCustomerQuery(CustomerSampleData.Id)
            );

            Assert.That(customer, Is.Not.Null);
            Assert.That(customer.Id, Is.EqualTo(CustomerSampleData.Id));
            Assert.That(customer.Email, Is.EqualTo(CustomerSampleData.Email));
            Assert.That(customer.FirstName, Is.EqualTo(CustomerSampleData.FirstName));
            Assert.That(customer.LastName, Is.EqualTo(CustomerSampleData.LastName));
        }

        [Test]
        public async Task RegisterCustomerTest()
        {
            await CustomersModule.ExecuteCommandAsync(new RegisterNewCustomerCommand(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName
            ));

            var registration = await CustomersModule.ExecuteQueryAsync(new GetCustomerRegistrationQuery(
                CustomerSampleData.Id
            ));

            await CustomersModule.ExecuteCommandAsync(new ConfirmRegistrationCommand(
                CustomerSampleData.Id,
                registration.ActivationCode
            ));

            var customer = await CustomersModule.ExecuteQueryAsync(
                new GetCustomerQuery(CustomerSampleData.Id)
            );

            Assert.That(customer, Is.Not.Null);
            Assert.That(customer.Id, Is.EqualTo(CustomerSampleData.Id));
            Assert.That(customer.Email, Is.EqualTo(CustomerSampleData.Email));
            Assert.That(customer.FirstName, Is.EqualTo(CustomerSampleData.FirstName));
            Assert.That(customer.LastName, Is.EqualTo(CustomerSampleData.LastName));
            Assert.That(customer.Password, Is.EqualTo(registration.Password));
        }
    }
}