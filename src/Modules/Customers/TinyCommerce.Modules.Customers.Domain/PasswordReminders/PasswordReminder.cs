using System;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders.Events;
using TinyCommerce.Modules.Customers.Domain.SeedWork;

namespace TinyCommerce.Modules.Customers.Domain.PasswordReminders
{
    public class PasswordReminder : Entity
    {
        private PasswordReminder()
        {
            // Entity framework
        }

        private PasswordReminder(
            PasswordReminderId id,
            string email,
            IResetCodeGenerator resetCodeGenerator
        )
        {
            Id = id;
            Email = email;
            Code = resetCodeGenerator.Generate();
            _requestedAt = SystemClock.Now;
            _expirationDate = SystemClock.Now.AddHours(48);

            AddDomainEvent(new PasswordReminderCreatedDomainEvent(
                Id,
                Email,
                Code,
                _requestedAt,
                _expirationDate
            ));
        }

        public PasswordReminderId Id { get; }
        public string Email { get; }
        public string Code { get; }
        private DateTime _requestedAt;
        private DateTime _expirationDate;

        public bool HasExpired()
        {
            return _expirationDate < SystemClock.Now;
        }
        
        public static PasswordReminder Create(
            PasswordReminderId id,
            string email,
            IResetCodeGenerator resetCodeGenerator
        )
        {
            return new PasswordReminder(id, email, resetCodeGenerator);
        }
    }
}