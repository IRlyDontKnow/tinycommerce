using Autofac;
using TinyCommerce.Modules.Catalog.Application.Categories;
using TinyCommerce.Modules.Catalog.Domain.Categories;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Configuration.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryCounter>()
                .As<ICategoryCounter>()
                .InstancePerLifetimeScope();
        }
    }
}