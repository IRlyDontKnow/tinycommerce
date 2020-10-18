using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using TinyCommerce.BuildingBlocks.Testing.Integration;
using TinyCommerce.Modules.Catalog.Application.Contracts;
using TinyCommerce.Modules.Catalog.Infrastructure;
using TinyCommerce.Modules.Catalog.Infrastructure.Configuration;
using TinyCommerce.Modules.Catalog.Infrastructure.Purging.PurgeModule;

namespace TinyCommerce.Modules.Catalog.Tests.Integration.SeedWork
{
    public abstract class TestBase
    {
        protected ICatalogModule CatalogModule;
        protected ILogger Logger;

        [SetUp]
        public async Task BeforeEachTest()
        {
            var connectionString =
                EnvironmentVariablesProvider.GetVariable("TinyCommerce_IntegrationTest_ConnectionString");

            if (connectionString == null)
                throw new ApplicationException($"You must provide connection string.");

            Logger = Substitute.For<ILogger>();

            CatalogStartup.Initialize(connectionString, Logger);
            CatalogModule = new CatalogModule();

            await CatalogModule.ExecuteCommandAsync(new PurgeModuleCommand());
        }

        [TearDown]
        public void AfterEachTest()
        {
        }
    }
}