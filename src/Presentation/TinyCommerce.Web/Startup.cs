using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using TinyCommerce.BuildingBlocks.Application.Emails;
using TinyCommerce.Modules.Catalog.Infrastructure.Configuration;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration;
using TinyCommerce.Web.Configuration.Emails;
using TinyCommerce.Web.Configuration.Logging;
using TinyCommerce.Web.Configuration.Modules;

namespace TinyCommerce.Web
{
    public class Startup
    {
        private ILogger _logger;
        private ILogger _loggerForWeb;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ConfigureLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRazorPages();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new ModulesModule());
            containerBuilder.RegisterModule(new LoggingModule(_loggerForWeb));
            containerBuilder.RegisterModule(new EmailModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            StartupModules(app);
        }

        private void StartupModules(IApplicationBuilder app)
        {
            var connectionString = Configuration["Database:ConnectionString"];
            var emailSender = app.ApplicationServices.GetService<IEmailSender>();

            CustomersStartup.Initialize(connectionString, _logger, emailSender);
            CatalogStartup.Initialize(connectionString, _logger);
        }

        private void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    path: "logs/logs",
                    rollingInterval: RollingInterval.Day,
                    formatter: new CompactJsonFormatter()
                )
                .CreateLogger();

            _loggerForWeb = _logger.ForContext("Presentation", "Web");
            _loggerForWeb.Information("Logger configured!");
        }
    }
}