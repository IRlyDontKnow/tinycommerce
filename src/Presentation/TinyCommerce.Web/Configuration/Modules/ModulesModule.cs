using Autofac;
using TinyCommerce.Modules.Catalog.Application.Contracts;
using TinyCommerce.Modules.Catalog.Infrastructure;
using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Infrastructure;

namespace TinyCommerce.Web.Configuration.Modules
{
    public class ModulesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomersModule>()
                .As<ICustomersModule>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CatalogModule>()
                .As<ICatalogModule>()
                .InstancePerLifetimeScope();
        }
    }
}