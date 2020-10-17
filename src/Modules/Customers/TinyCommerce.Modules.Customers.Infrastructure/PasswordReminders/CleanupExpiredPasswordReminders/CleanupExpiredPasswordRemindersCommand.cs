using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing;

namespace TinyCommerce.Modules.Customers.Infrastructure.PasswordReminders.CleanupExpiredPasswordReminders
{
    public class CleanupExpiredPasswordRemindersCommand : CommandBase, IRecurringCommand
    {
    }
}