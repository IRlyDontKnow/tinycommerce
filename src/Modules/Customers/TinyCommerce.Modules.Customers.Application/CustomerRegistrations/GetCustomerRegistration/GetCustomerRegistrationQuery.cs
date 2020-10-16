using System;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.GetCustomerRegistration
{
    public class GetCustomerRegistrationQuery : IQuery<CustomerRegistrationDto>
    {
        public GetCustomerRegistrationQuery(Guid customerRegistrationId)
        {
            CustomerRegistrationId = customerRegistrationId;
        }

        public Guid CustomerRegistrationId { get; }
    }
}