using System;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;

namespace TinyCommerce.Modules.BackOffice.Tests.Unit.Domain.Administrators
{
    public static class AdministratorSampleData
    {
        public static readonly AdministratorId Id = new AdministratorId(Guid.NewGuid());
        public const string Email = "admin@squadore.com";
        public const string Password = "admin123";
        public const string FirstName = "John";
        public const string LastName = "Doe";
        public const string Role = "BusinessOwner";
    }
}