using System;
using FluentValidation;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.Customers.ChangePassword
{
    public class ChangePasswordCommand : CommandBase
    {
        public ChangePasswordCommand(Guid customerId, string newPassword, string oldPassword)
        {
            CustomerId = customerId;
            NewPassword = newPassword;
            OldPassword = oldPassword;
        }

        public Guid CustomerId { get; }
        public string NewPassword { get; }
        public string OldPassword { get; }
    }

    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotNull();
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.OldPassword).NotEmpty();
        }
    }
}
