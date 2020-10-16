using NetArchTest.Rules;
using NUnit.Framework;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Customers.Tests.Arch.SeedWork;

namespace TinyCommerce.Modules.Customers.Tests.Arch.Domain
{
    [TestFixture]
    public class DomainTests : TestBase
    {
        [Test]
        public void DomainEvent_ShouldBeImmutable()
        {
            var types = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(DomainEventBase))
                .Or()
                .ImplementInterface(typeof(IDomainEvent))
                .GetTypes();

            AssertAreImmutable(types);
        }
    }
}