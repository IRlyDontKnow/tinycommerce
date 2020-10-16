using System;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.Customers.GetCustomer
{
    public class GetCustomerQuery : IQuery<CustomerDto>
    {
        public GetCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
