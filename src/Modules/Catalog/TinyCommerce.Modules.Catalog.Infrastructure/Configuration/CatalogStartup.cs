using Autofac;
using Serilog;

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

            var container = containerBuilder.Build();

            CatalogCompositionRoot.SetContainer(container);
        }

        public static void Stop()
        {
        }
    }
}