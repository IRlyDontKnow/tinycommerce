using System;
using System.Threading.Tasks;
using NUnit.Framework;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.ConfirmRegistration;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.GetCustomerRegistration;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.RegisterNewCustomer;
using TinyCommerce.Modules.Customers.Tests.Integration.Customers;
using TinyCommerce.Modules.Customers.Tests.Integration.SeedWork;

namespace TinyCommerce.Modules.Customers.Tests.Integration.CustomerRegistrations
{
    [TestFixture]
    public class CustomerRegistrationTests : TestBase
    {
        [Test]
        public async Task RegisterCustomer_LifecycleTest()
        {
            await CustomersModule.ExecuteCommandAsync(new RegisterNewCustomerCommand(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName
            ));

            var customerRegistration = await CustomersModule.ExecuteQueryAsync(
                new GetCustomerRegistrationQuery(CustomerSampleData.Id)
            );

            Assert.That(customerRegistration, Is.Not.Null);

            await CustomersModule.ExecuteCommandAsync(new ConfirmRegistrationCommand(
                CustomerSampleData.Id,
                customerRegistration.ActivationCode
            ));
        }
    }
}