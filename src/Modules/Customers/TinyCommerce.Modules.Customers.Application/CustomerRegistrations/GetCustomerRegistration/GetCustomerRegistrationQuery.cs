using System;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.GetCustomerRegistration
{
    public class GetCustomerRegistrationQuery : IQuery<CustomerRegistrationDto>
    {
        public GetCustomerRegistrationQuery(Guid? customerRegistrationId = null, string email = null)
        {
            CustomerRegistrationId = customerRegistrationId;
            Email = email;
        }

        public Guid? CustomerRegistrationId { get; }
        
        public string Email { get; }
    }
}