using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations
{
    public class CustomerRegistrationId : TypedIdValueBase
    {
        public CustomerRegistrationId(Guid value) : base(value)
        {
        }
    }
}