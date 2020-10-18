using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetArchTest.Rules;
using NUnit.Framework;
using TinyCommerce.Modules.BackOffice.Application.Contracts;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;
using TinyCommerce.Modules.BackOffice.Infrastructure;

namespace TinyCommerce.Modules.BackOffice.Tests.Arch.SeedWork
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;
        protected static Assembly DomainAssembly => typeof(Administrator).Assembly;
        protected static Assembly InfrastructureAssembly => typeof(BackOfficeModule).Assembly;

        protected static void AssertAreImmutable(IEnumerable<Type> types)
        {
            var failingTypes = new List<Type>();
            foreach (var type in types)
            {
                if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
                {
                    failingTypes.Add(type);
                    break;
                }
            }

            AssertFailingTypes(failingTypes);
        }

        protected static void AssertFailingTypes(IEnumerable<Type> types)
        {
            Assert.That(types, Is.Null.Or.Empty);
        }

        protected static void AssertArchTestResult(TestResult result)
        {
            AssertFailingTypes(result.FailingTypes);
        }
    }
}