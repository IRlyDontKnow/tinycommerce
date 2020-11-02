using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetArchTest.Rules;
using NUnit.Framework;
using TinyCommerce.Modules.Catalog.Application.Contracts;
using TinyCommerce.Modules.Catalog.Domain.Categories;
using TinyCommerce.Modules.Catalog.Infrastructure;

namespace TinyCommerce.Modules.Catalog.Tests.Arch.SeedWork
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;
        protected static Assembly DomainAssembly => typeof(Category).Assembly;
        protected static Assembly InfrastructureAssembly => typeof(CatalogModule).Assembly;

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