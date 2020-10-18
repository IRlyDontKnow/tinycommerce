using Autofac;
using TinyCommerce.Modules.BackOffice.Application.Authentication;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.Application
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
        }
    }
}