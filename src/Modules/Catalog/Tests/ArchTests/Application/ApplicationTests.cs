using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetArchTest.Rules;
using Newtonsoft.Json;
using NUnit.Framework;
using TinyCommerce.BuildingBlocks.Application.Events;
using TinyCommerce.Modules.Catalog.Application.Configuration;
using TinyCommerce.Modules.Catalog.Application.Contracts;
using TinyCommerce.Modules.Catalog.Tests.Arch.SeedWork;

namespace TinyCommerce.Modules.Catalog.Tests.Arch.Application
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
        
        [Test]
        public void DomainEventNotification_Constructor__ShouldHave_JsonConstructorAttribute()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEventNotification<>))
                .Or().ImplementInterface(typeof(IDomainEventNotification))
                .Or().Inherit(typeof(DomainNotificationBase<>))
                .GetTypes().ToList();

            AssertGivenTypes_HaveConstructor_WithJsonConstructorAttribute(types);
        }
        
        [Test]
        public void InternalCommand_Constructor_ShouldHave_JsonConstructorAttribute()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That().Inherit(typeof(InternalCommandBase))
                .GetTypes();

            AssertGivenTypes_HaveConstructor_WithJsonConstructorAttribute(types);
        }

        
        private void AssertGivenTypes_HaveConstructor_WithJsonConstructorAttribute(IEnumerable<Type> types)
        {
            var failingTypes = new List<Type>();

            foreach (var type in types)
            {
                var constructors = type.GetConstructors(
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Instance
                );

                var hasJsonConstructorAttribute = constructors
                    .Select(constructorInfo =>
                        constructorInfo.GetCustomAttributes(typeof(JsonConstructorAttribute), false))
                    .Any(jsonConstructorAttribute => jsonConstructorAttribute.Length > 0);

                if (!hasJsonConstructorAttribute)
                {
                    failingTypes.Add(type);
                }
            }

            AssertFailingTypes(failingTypes);
        }
    }
}