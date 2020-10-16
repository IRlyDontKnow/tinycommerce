using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}