using NetArchTest.Rules;
using NUnit.Framework;
using TinyCommerce.Modules.BackOffice.Application.Configuration;
using TinyCommerce.Modules.BackOffice.Application.Contracts;
using TinyCommerce.Modules.BackOffice.Tests.Arch.SeedWork;

namespace TinyCommerce.Modules.BackOffice.Tests.Arch.Application
{
    [TestFixture]
    public class ApplicationTests : TestBase
    {
        [Test]
        public void Command_ShouldBeImmutable()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That()
                .Inherit(typeof(CommandBase))
                .Or().Inherit(typeof(InternalCommandBase))
                .Or().ImplementInterface(typeof(ICommand))
                .GetTypes();

            AssertAreImmutable(types);
        }

        [Test]
        public void Query_ShouldBeImmutable()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That().ImplementInterface(typeof(IQuery<>))
                .GetTypes();

            AssertAreImmutable(types);
        }

        [Test]
        public void CommandHandler_ClassName_ShouldEndWith_CommandHandler()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Should().HaveNameEndingWith("CommandHandler")
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void QueryHandler_ClassName_ShouldEndWith_QueryHandler()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should().HaveNameEndingWith("QueryHandler")
                .GetResult();

            AssertArchTestResult(result);
        }
    }
}