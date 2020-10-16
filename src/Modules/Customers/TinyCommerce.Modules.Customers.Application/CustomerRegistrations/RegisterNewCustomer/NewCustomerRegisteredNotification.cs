using System;
using Newtonsoft.Json;
using TinyCommerce.BuildingBlocks.Application.Events;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Events;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.RegisterNewCustomer
{
    public class NewCustomerRegisteredNotification : DomainNotificationBase<NewCustomerRegisteredDomainEvent>
    {
        [JsonConstructor]
        public NewCustomerRegisteredNotification(NewCustomerRegisteredDomainEvent domainEvent, Guid id) : base(
            domainEvent, id)
        {
        }
    }
}