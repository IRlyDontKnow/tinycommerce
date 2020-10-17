using System;
using TinyCommerce.Modules.Customers.Domain.Customers;

namespace TinyCommerce.Modules.Customers.Tests.Domain.Customers
{
    public class CustomerSampleData
    {
        public static readonly CustomerId Id = new CustomerId(Guid.NewGuid());
        public const string Email = "john@squadore.com";
        public const string Password = "john123";
        public const string FirstName = "John";
        public const string LastName = "Doe";
        public static readonly DateTime RegistrationDate = DateTime.Now;
    }
}