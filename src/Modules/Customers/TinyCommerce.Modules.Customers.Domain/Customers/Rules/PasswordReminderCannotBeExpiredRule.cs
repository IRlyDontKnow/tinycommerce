using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;

namespace TinyCommerce.Modules.Customers.Domain.Customers.Rules
{
    public class PasswordReminderCannotBeExpiredRule : IBusinessRule
    {
        private readonly PasswordReminder _passwordReminder;

        public PasswordReminderCannotBeExpiredRule(PasswordReminder passwordReminder)
        {
            _passwordReminder = passwordReminder;
        }

        public bool IsBroken()
        {
            return _passwordReminder.HasExpired();
        }

        public string Message => "Password reminder cannot be expired rule.";
    }
}