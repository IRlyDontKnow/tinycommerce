using System;

namespace TinyCommerce.Modules.Customers.Application.Authentication
{
    public class AuthCustomerDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}