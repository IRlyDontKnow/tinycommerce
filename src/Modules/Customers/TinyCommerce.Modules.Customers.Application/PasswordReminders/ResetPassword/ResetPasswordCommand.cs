using FluentValidation;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.PasswordReminders.ResetPassword
{
    public class ResetPasswordCommand : CommandBase
    {
        public ResetPasswordCommand(string email, string resetCode, string newPassword)
        {
            Email = email;
            ResetCode = resetCode;
            NewPassword = newPassword;
        }

        public string Email { get; }
        public string ResetCode { get; }
        public string NewPassword { get; }
    }

    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.ResetCode).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
        }
    }
}