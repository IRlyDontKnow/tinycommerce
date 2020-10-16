using System;
using FluentValidation;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.RegisterNewCustomer
{
    public class RegisterNewCustomerCommand : CommandBase
    {
        public RegisterNewCustomerCommand(
            Guid customerRegistrationId,
            string email,
            string password,
            string firstName,
            string lastName
        )
        {
            CustomerRegistrationId = customerRegistrationId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid CustomerRegistrationId { get; }
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }

    public class RegisterNewCustomerCommandValidation : AbstractValidator<RegisterNewCustomerCommand>
    {
        public RegisterNewCustomerCommandValidation()
        {
            RuleFor(f => f.CustomerRegistrationId).NotNull();
            RuleFor(f => f.Email).NotEmpty().EmailAddress();
            RuleFor(f => f.Password).NotEmpty();
            RuleFor(f => f.FirstName).NotEmpty();
            RuleFor(f => f.LastName).NotEmpty();
        }
    }
}