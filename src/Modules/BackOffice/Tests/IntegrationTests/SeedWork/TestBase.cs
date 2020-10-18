using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using TinyCommerce.BuildingBlocks.Testing.Integration;
using TinyCommerce.Modules.BackOffice.Application.Contracts;
using TinyCommerce.Modules.BackOffice.Infrastructure;
using TinyCommerce.Modules.BackOffice.Infrastructure.Configuration;
using TinyCommerce.Modules.BackOffice.Infrastructure.Purging.PurgeModule;

namespace TinyCommerce.Modules.BackOffice.Tests.Integration.SeedWork
{
    public abstract class TestBase
    {
        protected IBackOfficeModule BackOfficeModule;
        protected ILogger Logger;

        [SetUp]
        public async Task BeforeEachTest()
        {
            var connectionString =
                EnvironmentVariablesProvider.GetVariable("TinyCommerce_IntegrationTest_ConnectionString");

            if (connectionString == null)
                throw new ApplicationException($"You must provide connection string.");

            Logger = Substitute.For<ILogger>();

            BackOfficeStartup.Initialize(connectionString, Logger);
            BackOfficeModule = new BackOfficeModule();

            await BackOfficeModule.ExecuteCommandAsync(new PurgeModuleCommand());
        }

        [TearDown]
        public void AfterEachTest()
        {
            BackOfficeStartup.Stop();
        }
    }
}