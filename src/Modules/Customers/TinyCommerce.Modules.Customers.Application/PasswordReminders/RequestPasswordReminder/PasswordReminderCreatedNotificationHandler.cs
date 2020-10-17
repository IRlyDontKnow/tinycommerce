using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.BuildingBlocks.Application.Emails;

namespace TinyCommerce.Modules.Customers.Application.PasswordReminders.RequestPasswordReminder
{
    public class PasswordReminderCreatedNotificationHandler : INotificationHandler<PasswordReminderCreatedNotification>
    {
        private readonly IEmailSender _emailSender;

        public PasswordReminderCreatedNotificationHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(PasswordReminderCreatedNotification notification, CancellationToken cancellationToken)
        {
            await _emailSender.SendAsync(new EmailMessage(
                notification.DomainEvent.Email,
                "Password recovery",
                $"Here is your code: {notification.DomainEvent.Code}"
            ));
        }
    }
}