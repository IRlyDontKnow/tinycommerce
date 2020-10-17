using System;
using Newtonsoft.Json;
using TinyCommerce.BuildingBlocks.Application.Events;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders.Events;

namespace TinyCommerce.Modules.Customers.Application.PasswordReminders.RequestPasswordReminder
{
    public class PasswordReminderCreatedNotification : DomainNotificationBase<PasswordReminderCreatedDomainEvent>
    {
        [JsonConstructor]
        public PasswordReminderCreatedNotification(PasswordReminderCreatedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}