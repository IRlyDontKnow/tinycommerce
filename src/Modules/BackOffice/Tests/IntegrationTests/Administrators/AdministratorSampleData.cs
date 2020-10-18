using System;

namespace TinyCommerce.Modules.BackOffice.Tests.Integration.Administrators
{
    public class AdministratorSampleData
    {
        public static readonly Guid Id = Guid.Parse("e0668e9d-e566-4065-911b-07685db2162b");
        public const string Email = "admin@squadore.com";
        public const string Password = "admin";
        public const string FirstName = "John";
        public const string LastName = "Doe";
        public const string Role = "BusinessOwner";
    }
}