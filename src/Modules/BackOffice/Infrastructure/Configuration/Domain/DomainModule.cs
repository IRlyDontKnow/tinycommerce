using Autofac;
using TinyCommerce.Modules.BackOffice.Application.Administrators;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdministratorCounter>()
                .As<IAdministratorCounter>()
                .InstancePerLifetimeScope();
        }
    }
}