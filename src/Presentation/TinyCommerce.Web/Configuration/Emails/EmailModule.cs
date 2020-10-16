using Autofac;
using TinyCommerce.BuildingBlocks.Application.Emails;
using TinyCommerce.BuildingBlocks.Infrastructure.Emails;

namespace TinyCommerce.Web.Configuration.Emails
{
    public class EmailModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogEmailSender>()
                .As<IEmailSender>()
                .SingleInstance();
        }
    }
}