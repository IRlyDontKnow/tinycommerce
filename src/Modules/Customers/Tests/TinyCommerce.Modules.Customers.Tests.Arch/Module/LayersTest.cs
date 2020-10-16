using NetArchTest.Rules;
using NUnit.Framework;
using TinyCommerce.Modules.Customers.Tests.Arch.SeedWork;

namespace TinyCommerce.Modules.Customers.Tests.Arch.Module
{
    [TestFixture]
    public class LayersTest : TestBase
    {
        [Test]
        public void DomainLayer_DoesNotHaveDependency_ToApplicationLayer()
        {
            var result = Types.InAssembly(DomainAssembly)
                .Should()
                .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void DomainLayer_DoesNotHaveDependency_ToInfrastructureLayer()
        {
            var result = Types.InAssembly(DomainAssembly)
                .Should()
                .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void ApplicationLayer_DoesNotHaveDependency_ToInfrastructureLayer()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .Should()
                .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
                .GetResult();

            AssertArchTestResult(result);
        }
    }
}