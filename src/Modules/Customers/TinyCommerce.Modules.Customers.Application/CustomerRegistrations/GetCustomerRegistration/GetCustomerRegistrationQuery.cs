using System;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.GetCustomerRegistration
{
    public class GetCustomerRegistrationQuery : IQuery<CustomerRegistrationDto>
    {
        public Guid? CustomerRegistrationId { get; private set; }

        public string Email { get; private set; }

        public static GetCustomerRegistrationQuery ByEmail(string email)
        {
            return new GetCustomerRegistrationQuery
            {
                Email = email
            };
        }

        public static GetCustomerRegistrationQuery ById(Guid id)
        {
            return new GetCustomerRegistrationQuery
            {
                CustomerRegistrationId = id
            };
        }
    }
}