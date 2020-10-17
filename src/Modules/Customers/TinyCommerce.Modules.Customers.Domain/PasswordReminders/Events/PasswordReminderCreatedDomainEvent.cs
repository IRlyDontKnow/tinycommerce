using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.PasswordReminders.Events
{
    public class PasswordReminderCreatedDomainEvent : DomainEventBase
    {
        public PasswordReminderCreatedDomainEvent(
            PasswordReminderId passwordReminderId,
            string email,
            string code,
            DateTime requestedAt,
            DateTime expirationDate
        )
        {
            PasswordReminderId = passwordReminderId;
            Email = email;
            Code = code;
            RequestedAt = requestedAt;
            ExpirationDate = expirationDate;
        }

        public PasswordReminderId PasswordReminderId { get; }
        public string Email { get; }
        public string Code { get; }
        public DateTime RequestedAt { get; }
        public DateTime ExpirationDate { get; }
    }
}