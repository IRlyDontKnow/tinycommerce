using System;
using System.Collections.Generic;
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
        private static readonly CliOptions Options = new CliOptions();

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

            var serviceProvider = CreateServices(Options.ConnectionString);

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                if (Options.MigrateUp)
                {
                    runner.MigrateUp();
                }
            }

            logger.Information("Migration done!");

            return 0;
        }

        private static List<string> ParseOptions(string[] args)
        {
            var optionSet = new OptionSet
            {
                {"cs|connectionString=", "database connection string", v => { Options.ConnectionString = v; }},
                {"p|profile=", "fluent migrator profile", v => { Options.Profile = v; }},
                {"up", "migrate up", v => { Options.MigrateUp = v != null; }},
                {"down", "migrate down", v => { Options.MigrateDown = v != null; }},
                {"h|help", "prints the help", v => { Options.ShouldShowHelp = v != null; }},
                {
                    "task=|t=",
                    "The task you want FluentMigrator to perform. Available choices are: migrate:up, migrate (same as migrate:up), migrate:down, rollback, rollback:toversion, rollback:all, validateversionorder, listmigrations. Default is 'migrate'.",
                    v => { Options.Task = v; }
                },
            };

            return optionSet.Parse(args);
        }

        private static void ShowHelp(OptionSet optionSet)
        {
            optionSet.WriteOptionDescriptions(Console.Out);
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
                .Configure<RunnerOptions>(options => { options.Profile = Options.Profile; })
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }

    public class CliOptions
    {
        public string Task { get; set; }

        public string ConnectionString { get; set; }

        public string Profile { get; set; }

        public bool MigrateUp { get; set; }

        public bool MigrateDown { get; set; }

        public bool ShouldShowHelp { get; set; }
    }
}