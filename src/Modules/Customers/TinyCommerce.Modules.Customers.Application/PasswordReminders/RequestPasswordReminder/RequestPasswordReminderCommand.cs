using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.PasswordReminders.RequestPasswordReminder
{
    public class RequestPasswordReminderCommand : CommandBase
    {
        public RequestPasswordReminderCommand(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}