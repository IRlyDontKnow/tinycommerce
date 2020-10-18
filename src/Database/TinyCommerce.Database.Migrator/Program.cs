using System;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Mono.Options;
using Serilog;
using TinyCommerce.Database.Migrator.Migrations;

namespace TinyCommerce.Database.Migrator
{
    class Program
    {
        private static string Profile { get; set; }

        static int Main(string[] args)
        {
            try
            {
                ParseOptions(args);
            }
            catch (OptionException ex)
            {
                Console.WriteLine(@"Try '--help' for more information.");
                return 2;
            }

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            logger.Information("Starting migration...");

            if (args.Length < 1)
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

        private static void ParseOptions(string[] args)
        {
            var optionSet = new OptionSet
            {
                {
                    "p|profile=",
                    "fluent migrator profile",
                    v => { Profile = v; }
                }
            };

            optionSet.Parse(args);
        }

        private static IServiceProvider CreateServices(string connectionString, string profile = null)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                    rb.AddPostgres()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(Migration1).Assembly).For.Migrations()
                )
                .Configure<RunnerOptions>(options =>
                {
                    options.Profile = Profile;
                })
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}