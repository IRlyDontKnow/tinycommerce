using System;

namespace TinyCommerce.Modules.Customers.Application.Customers.GetCustomer
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}