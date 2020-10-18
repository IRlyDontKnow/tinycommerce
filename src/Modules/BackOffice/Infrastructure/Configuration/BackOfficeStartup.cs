using Autofac;
using Serilog;
using TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.Application;
using TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.DataAccess;
using TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.Domain;
using TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.Logging;
using TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.Mediation;
using TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.Processing;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Configuration
{
    public static class BackOfficeStartup
    {
        public static void Initialize(string connectionString, ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "BackOffice");

            ConfigureCompositionRoot(connectionString, moduleLogger);
        }

        private static void ConfigureCompositionRoot(string connectionString, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ApplicationModule());
            containerBuilder.RegisterModule(new ProcessingModule());

            var container = containerBuilder.Build();

            BackOfficeCompositionRoot.SetContainer(container);
        }

        public static void Stop()
        {
        }
    }
}