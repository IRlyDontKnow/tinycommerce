using NSubstitute;
using NUnit.Framework;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;
using TinyCommerce.Modules.BackOffice.Domain.Administrators.Events;
using TinyCommerce.Modules.BackOffice.Domain.Administrators.Rules;
using TinyCommerce.Modules.BackOffice.Tests.Unit.Domain.SeedWork;

namespace TinyCommerce.Modules.BackOffice.Tests.Unit.Domain.Administrators
{
    [TestFixture]
    public class AdministratorTests : TestBase
    {
        [Test]
        public void CreateNew_Test()
        {
            var administratorCounter = Substitute.For<IAdministratorCounter>();
            administratorCounter.CountByEmail(AdministratorSampleData.Email).Returns(0);

            var administrator = Administrator.CreateNew(
                AdministratorSampleData.Id,
                AdministratorSampleData.Email,
                AdministratorSampleData.Password,
                AdministratorSampleData.FirstName,
                AdministratorSampleData.LastName,
                AdministratorSampleData.Role,
                administratorCounter
            );

            var domainEvent = AssertPublishedDomainEvent<AdministratorCreatedDomainEvent>(administrator);

            Assert.That(domainEvent.AdministratorId, Is.EqualTo(AdministratorSampleData.Id));
            Assert.That(domainEvent.Email, Is.EqualTo(AdministratorSampleData.Email));
            Assert.That(domainEvent.Password, Is.EqualTo(AdministratorSampleData.Password));
            Assert.That(domainEvent.FirstName, Is.EqualTo(AdministratorSampleData.FirstName));
            Assert.That(domainEvent.LastName, Is.EqualTo(AdministratorSampleData.LastName));
            Assert.That(domainEvent.Role, Is.EqualTo(AdministratorSampleData.Role));
        }

        [Test]
        public void CreateNew_WithAlreadyUsedEmail_ShouldFail()
        {
            var administratorCounter = Substitute.For<IAdministratorCounter>();
            administratorCounter.CountByEmail(AdministratorSampleData.Email).Returns(1);

            AssertBrokenRule<AdministratorEmailMustBeUniqueRule>(() =>
            {
                Administrator.CreateNew(
                    AdministratorSampleData.Id,
                    AdministratorSampleData.Email,
                    AdministratorSampleData.Password,
                    AdministratorSampleData.FirstName,
                    AdministratorSampleData.LastName,
                    AdministratorSampleData.Role,
                    administratorCounter
                );
            });
        }
    }
}