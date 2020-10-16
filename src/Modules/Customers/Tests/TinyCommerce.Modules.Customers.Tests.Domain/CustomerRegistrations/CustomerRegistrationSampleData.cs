using System;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations;

namespace TinyCommerce.Modules.Customers.Tests.Domain.CustomerRegistrations
{
    public static class CustomerRegistrationSampleData
    {
        public static CustomerRegistrationId Id = new CustomerRegistrationId(Guid.NewGuid());
        public const string Email = "customer@squadore.com";
        public const string FirstName = "John";
        public const string LastName = "Doe";
        public const string Password = "pass123";
    }
}
