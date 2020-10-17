using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;

namespace TinyCommerce.Modules.Customers.Domain.Customers.Events
{
    public class CustomerPasswordResetDomainEvent : DomainEventBase
    {
        public CustomerPasswordResetDomainEvent(
            PasswordReminderId passwordReminderId,
            CustomerId customerId,
            string newPassword
        )
        {
            PasswordReminderId = passwordReminderId;
            CustomerId = customerId;
            NewPassword = newPassword;
        }

        public PasswordReminderId PasswordReminderId { get; }
        public CustomerId CustomerId { get; }
        public string NewPassword { get; }
    }
}