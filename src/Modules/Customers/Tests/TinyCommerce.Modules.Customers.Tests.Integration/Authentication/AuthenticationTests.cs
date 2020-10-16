using System.Threading.Tasks;
using NUnit.Framework;
using TinyCommerce.Modules.Customers.Application.Customers.CreateCustomer;
using TinyCommerce.Modules.Customers.Tests.Integration.Customers;
using TinyCommerce.Modules.Customers.Tests.Integration.SeedWork;

namespace TinyCommerce.Modules.Customers.Tests.Integration.Authentication
{
    [TestFixture]
    public class AuthenticationTests : TestBase
    {
        [Test]
        public async Task Authenticate_WithValidCredentials_ShouldSucceed()
        {
            await CustomersModule.ExecuteCommandAsync(new CreateCustomerCommand(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName
            ));

            var authenticationResult = await CustomersModule.Authenticate(
                CustomerSampleData.Email,
                CustomerSampleData.Password
            );

            Assert.That(authenticationResult.Authenticated, Is.True);
            Assert.That(authenticationResult.Customer, Is.Not.Null);
            Assert.That(authenticationResult.Customer.Id, Is.EqualTo(CustomerSampleData.Id));
        }

        [Test]
        public async Task Authenticate_WithInvalidEmail_ShouldFail()
        {
            await CustomersModule.ExecuteCommandAsync(new CreateCustomerCommand(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName
            ));

            var authenticationResult = await CustomersModule.Authenticate(
                "invalid124@squadore.com",
                CustomerSampleData.Password
            );

            Assert.That(authenticationResult.Authenticated, Is.False);
            Assert.That(authenticationResult.Error, Is.EqualTo("Invalid credentials"));
        }

        [Test]
        public async Task Authenticate_WithInvalidPassword_ShouldFail()
        {
            await CustomersModule.ExecuteCommandAsync(new CreateCustomerCommand(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName
            ));

            var authenticationResult = await CustomersModule.Authenticate(
                CustomerSampleData.Email,
                "invalidPass1237"
            );

            Assert.That(authenticationResult.Authenticated, Is.False);
            Assert.That(authenticationResult.Error, Is.EqualTo("Invalid credentials"));
        }
    }
}