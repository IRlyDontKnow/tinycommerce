using System;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.BackOffice.Domain.Administrators.Events;
using TinyCommerce.Modules.BackOffice.Domain.Administrators.Rules;

namespace TinyCommerce.Modules.BackOffice.Domain.Administrators
{
    public class Administrator : Entity, IAggregateRoot
    {
        private Administrator()
        {
            // Entity framework
        }

        public Administrator(
            AdministratorId id,
            string email,
            string password,
            string firstName,
            string lastName,
            string role,
            IAdministratorCounter counter
        )
        {
            CheckRule(new AdministratorEmailMustBeUniqueRule(email, counter));

            Id = id;
            _email = email;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _role = role;
            _createdAt = DateTime.UtcNow;

            AddDomainEvent(new AdministratorCreatedDomainEvent(
                Id,
                _email,
                _password,
                _firstName,
                _lastName,
                _role,
                _createdAt
            ));
        }

        public AdministratorId Id { get; }
        private string _email;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _role;
        private DateTime _createdAt;

        public static Administrator CreateNew(
            AdministratorId id,
            string email,
            string password,
            string firstName,
            string lastName,
            string role,
            IAdministratorCounter counter
        )
        {
            return new Administrator(id, email, password, firstName, lastName, role, counter);
        }
    }
}