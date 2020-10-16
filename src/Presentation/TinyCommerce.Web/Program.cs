using System.Collections.Generic;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TinyCommerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var switchMappings = new Dictionary<string, string>
                    {
                        {"--with-fixtures", "withFixtures"}
                    };

                    config.AddCommandLine(args, switchMappings);
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.${env.EnvironmentName}.json", true, true);
                    config.AddEnvironmentVariables("TINYCOMMERCE_");
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}