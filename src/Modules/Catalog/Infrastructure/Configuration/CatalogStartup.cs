using Autofac;
using Serilog;
using TinyCommerce.Modules.Catalog.Infrastructure.Configuration.DataAccess;
using TinyCommerce.Modules.Catalog.Infrastructure.Configuration.Domain;
using TinyCommerce.Modules.Catalog.Infrastructure.Configuration.Logging;
using TinyCommerce.Modules.Catalog.Infrastructure.Configuration.Mediation;
using TinyCommerce.Modules.Catalog.Infrastructure.Configuration.Processing;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Configuration
{
    public static class CatalogStartup
    {
        public static void Initialize(string connectionString, ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "Customers");

            ConfigureCompositionRoot(connectionString, moduleLogger);
        }

        private static void ConfigureCompositionRoot(string connectionString, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());

            var container = containerBuilder.Build();

            CatalogCompositionRoot.SetContainer(container);
        }

        public static void Stop()
        {
        }
    }
}