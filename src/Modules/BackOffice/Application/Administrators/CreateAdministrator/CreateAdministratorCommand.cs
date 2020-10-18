using System;
using FluentValidation;
using TinyCommerce.Modules.BackOffice.Application.Contracts;

namespace TinyCommerce.Modules.BackOffice.Application.Administrators.CreateAdministrator
{
    public class CreateAdministratorCommand : CommandBase
    {
        public CreateAdministratorCommand(
            Guid administratorId,
            string email,
            string password,
            string firstName,
            string lastName,
            string role
        )
        {
            AdministratorId = administratorId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public Guid AdministratorId { get; }
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Role { get; }
    }

    public class CreateAdministratorCommandValidator : AbstractValidator<CreateAdministratorCommand>
    {
        public CreateAdministratorCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}