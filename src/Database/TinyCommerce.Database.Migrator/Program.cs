using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TinyCommerce.Database.Migrator.Migrations;

namespace TinyCommerce.Database.Migrator
{
    class Program
    {
        static int Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            logger.Information("Starting migration...");

            if (args.Length != 1)
            {
                logger.Error("Invalid arguments. You must provide connection string.");
                logger.Information("Migration stopped.");
                return -1;
            }

            var connectionString = args[0];
            var serviceProvider = CreateServices(connectionString);

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }

            logger.Information("Migration done!");

            return 0;
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                    rb.AddPostgres()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(Migration1).Assembly).For.Migrations()
                )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}