using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.SendConfirmationEmail;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.RegisterNewCustomer
{
    public class NewCustomerRegisteredNotificationHandler : INotificationHandler<NewCustomerRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public NewCustomerRegisteredNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(NewCustomerRegisteredNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new SendConfirmationEmailCommand(
                Guid.NewGuid(),
                notification.DomainEvent.CustomerRegistrationId.Value
            ));
        }
    }
}