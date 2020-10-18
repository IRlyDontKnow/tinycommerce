using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.BackOffice.Domain.Administrators.Events
{
    public class AdministratorCreatedDomainEvent : DomainEventBase
    {
        public AdministratorCreatedDomainEvent(
            AdministratorId administratorId,
            string email,
            string password,
            string firstName,
            string lastName,
            string role,
            DateTime createdAt
        )
        {
            AdministratorId = administratorId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            CreatedAt = createdAt;
        }

        public AdministratorId AdministratorId { get; }
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Role { get; }
        public DateTime CreatedAt { get; }
    }
}