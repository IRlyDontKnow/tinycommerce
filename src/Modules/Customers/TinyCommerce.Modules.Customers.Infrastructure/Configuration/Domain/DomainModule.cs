using Autofac;
using TinyCommerce.Modules.Customers.Application.Customers;
using TinyCommerce.Modules.Customers.Application.PasswordReminders;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerChecker>()
                .As<ICustomerChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ResetCodeGenerator>()
                .As<IResetCodeGenerator>()
                .InstancePerLifetimeScope();
        }
    }
}