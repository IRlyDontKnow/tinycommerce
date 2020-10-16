using System;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.GetCustomerRegistration
{
    public class CustomerRegistrationDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ActivationCode { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
