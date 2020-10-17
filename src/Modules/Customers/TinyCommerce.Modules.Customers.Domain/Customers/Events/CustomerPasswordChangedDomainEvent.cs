using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.Customers.Events
{
    public class CustomerPasswordChangedDomainEvent : DomainEventBase
    {
        public CustomerPasswordChangedDomainEvent(
            CustomerId customerId,
            string oldPassword,
            string newPassword,
            DateTime changeDate
        )
        {
            CustomerId = customerId;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ChangeDate = changeDate;
        }

        public CustomerId CustomerId { get; }
        public string OldPassword { get; }
        public string NewPassword { get; }
        public DateTime ChangeDate { get; }
    }
}