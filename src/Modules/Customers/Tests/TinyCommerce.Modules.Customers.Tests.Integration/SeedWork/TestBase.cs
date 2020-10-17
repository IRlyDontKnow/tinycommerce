using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.BuildingBlocks.Application.Emails;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.BuildingBlocks.Infrastructure.DataAccess;
using TinyCommerce.BuildingBlocks.Testing.Integration;
using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Infrastructure;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration;
using TinyCommerce.Modules.Customers.Infrastructure.Purging.PurgeModule;

namespace TinyCommerce.Modules.Customers.Tests.Integration.SeedWork
{
    public abstract class TestBase
    {
        protected ICustomersModule CustomersModule;
        protected ILogger Logger;
        protected IEmailSender EmailSender;
        protected IDbConnectionFactory ConnectionFactory;

        [SetUp]
        public async Task BeforeEachTest()
        {
            var connectionString =
                EnvironmentVariablesProvider.GetVariable("TinyCommerce_IntegrationTest_ConnectionString");

            if (connectionString == null)
                throw new ApplicationException($"You must provide connection string.");

            Logger = Substitute.For<ILogger>();
            EmailSender = Substitute.For<IEmailSender>();
            ConnectionFactory = new PostgresConnectionFactory(connectionString);

            CustomersStartup.Initialize(connectionString, Logger, EmailSender);
            CustomersModule = new CustomersModule();

            await CustomersModule.ExecuteCommandAsync(new PurgeModuleCommand());
        }

        [TearDown]
        public void AfterEachTest()
        {
            CustomersStartup.Stop();
        }
    }
}