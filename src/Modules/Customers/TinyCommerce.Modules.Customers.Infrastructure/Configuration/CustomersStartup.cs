using Autofac;
using Serilog;
using TinyCommerce.BuildingBlocks.Application.Emails;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Application;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.DataAccess;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Domain;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Emails;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Logging;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Mediation;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Quartz;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration
{
    public static class CustomersStartup
    {
        public static void Initialize(string connectionString, ILogger logger, IEmailSender emailSender)
        {
            var moduleLogger = logger.ForContext("Module", "Customers");

            ConfigureCompositionRoot(connectionString, moduleLogger, emailSender);
            QuartzStartup.Start(moduleLogger);
        }

        private static void ConfigureCompositionRoot(string connectionString, ILogger logger, IEmailSender emailSender)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new EmailsModule(emailSender));
            containerBuilder.RegisterModule(new MediatrModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ApplicationModule());

            var container = containerBuilder.Build();

            CustomersCompositionRoot.SetContainer(container);
        }

        public static void Stop()
        {
            QuartzStartup.Stop();
        }
    }
}