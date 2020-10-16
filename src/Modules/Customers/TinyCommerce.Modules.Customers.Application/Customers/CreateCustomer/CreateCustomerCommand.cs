using System;
using FluentValidation;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.Customers.CreateCustomer
{
    public class CreateCustomerCommand : CommandBase
    {
        public CreateCustomerCommand(
            Guid customerId,
            string email,
            string password,
            string firstName,
            string lastName
        )
        {
            CustomerId = customerId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid CustomerId { get; }
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotNull();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}