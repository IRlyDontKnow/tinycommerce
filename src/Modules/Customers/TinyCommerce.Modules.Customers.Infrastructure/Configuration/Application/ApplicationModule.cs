using Autofac;
using TinyCommerce.Modules.Customers.Application.Authentication;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordHasher>()
                .As<IPasswordHasher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Authenticator>()
                .As<IAuthenticator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ActivationCodeGenerator>()
                .As<IActivationCodeGenerator>()
                .InstancePerLifetimeScope();
        }
    }
}