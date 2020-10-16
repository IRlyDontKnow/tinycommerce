using Autofac;
using TinyCommerce.BuildingBlocks.Application.Emails;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Emails
{
    public class EmailsModule : Module
    {
        private readonly IEmailSender _emailSender;

        public EmailsModule(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_emailSender)
                .As<IEmailSender>();
        }
    }
}