using System.Threading.Tasks;
using NUnit.Framework;
using TinyCommerce.Modules.BackOffice.Application.Administrators.CreateAdministrator;
using TinyCommerce.Modules.BackOffice.Tests.Integration.Administrators;
using TinyCommerce.Modules.BackOffice.Tests.Integration.SeedWork;

namespace TinyCommerce.Modules.BackOffice.Tests.Integration.Authentication
{
    [TestFixture]
    public class AuthenticationTests : TestBase
    {
        [Test]
        public async Task Authenticate_WithValidCredentials_ShouldSucceed()
        {
            await CreateSampleAdministrator();

            var authenticationResult = await BackOfficeModule.AuthenticateAdministrator(
                AdministratorSampleData.Email,
                AdministratorSampleData.Password
            );

            Assert.That(authenticationResult.Authenticated, Is.True);
            Assert.That(authenticationResult.Administrator, Is.Not.Null);
            Assert.That(authenticationResult.Administrator.Id, Is.EqualTo(AdministratorSampleData.Id));
        }

        [Test]
        public async Task Authenticate_WithInvalidEmail_ShouldFail()
        {
            var authenticationResult = await BackOfficeModule.AuthenticateAdministrator(
                "invalidEmail@squadore.com",
                AdministratorSampleData.Password
            );

            Assert.That(authenticationResult.Authenticated, Is.False);
            Assert.That(authenticationResult.Error, Is.EqualTo("Invalid credentials"));
        }

        [Test]
        public async Task Authenticate_WithInvalidPassword_ShouldFail()
        {
            await CreateSampleAdministrator();

            var authenticationResult = await BackOfficeModule.AuthenticateAdministrator(
                AdministratorSampleData.Email,
                "InvalidPass123"
            );

            Assert.That(authenticationResult.Authenticated, Is.False);
            Assert.That(authenticationResult.Error, Is.EqualTo("Invalid credentials"));
        }

        private async Task CreateSampleAdministrator()
        {
            await BackOfficeModule.ExecuteCommandAsync(new CreateAdministratorCommand(
                AdministratorSampleData.Id,
                AdministratorSampleData.Email,
                AdministratorSampleData.Password,
                AdministratorSampleData.FirstName,
                AdministratorSampleData.LastName,
                AdministratorSampleData.Role
            ));
        }
    }
}