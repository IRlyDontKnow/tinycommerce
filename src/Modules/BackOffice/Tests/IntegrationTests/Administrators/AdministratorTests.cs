using System.Threading.Tasks;
using NUnit.Framework;
using TinyCommerce.Modules.BackOffice.Application.Administrators.CreateAdministrator;
using TinyCommerce.Modules.BackOffice.Application.Administrators.GetAdministrator;
using TinyCommerce.Modules.BackOffice.Tests.Integration.SeedWork;

namespace TinyCommerce.Modules.BackOffice.Tests.Integration.Administrators
{
    [TestFixture]
    public class AdministratorTests : TestBase
    {
        [Test]
        public async Task CreateAdministrator_Test()
        {
            await BackOfficeModule.ExecuteCommandAsync(new CreateAdministratorCommand(
                AdministratorSampleData.Id,
                AdministratorSampleData.Email,
                AdministratorSampleData.Password,
                AdministratorSampleData.FirstName,
                AdministratorSampleData.LastName,
                AdministratorSampleData.Role
            ));

            var administrator = await BackOfficeModule.ExecuteQueryAsync(new GetAdministratorQuery(
                AdministratorSampleData.Id
            ));

            Assert.That(administrator, Is.Not.Null);
            Assert.That(administrator.Id, Is.EqualTo(AdministratorSampleData.Id));
            Assert.That(administrator.Email, Is.EqualTo(AdministratorSampleData.Email));
            Assert.That(administrator.Password, Is.Not.EqualTo(AdministratorSampleData.Password));
            Assert.That(administrator.FirstName, Is.EqualTo(AdministratorSampleData.FirstName));
            Assert.That(administrator.LastName, Is.EqualTo(AdministratorSampleData.LastName));
            Assert.That(administrator.Role, Is.EqualTo(AdministratorSampleData.Role));
        }
    }
}