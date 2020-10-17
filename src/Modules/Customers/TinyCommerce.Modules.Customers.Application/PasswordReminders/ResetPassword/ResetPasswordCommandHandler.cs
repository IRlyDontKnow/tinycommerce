using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.BuildingBlocks.Application;
using TinyCommerce.Modules.Customers.Application.Authentication;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;

namespace TinyCommerce.Modules.Customers.Application.PasswordReminders.ResetPassword
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
    {
        private readonly IPasswordReminderRepository _passwordReminder;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICustomerRepository _customerRepository;

        public ResetPasswordCommandHandler(
            IPasswordReminderRepository passwordReminder,
            IPasswordHasher passwordHasher,
            ICustomerRepository customerRepository
        )
        {
            _passwordReminder = passwordReminder;
            _passwordHasher = passwordHasher;
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var reminder = await _passwordReminder.GetByEmailAndCodeAsync(command.Email, command.ResetCode);

            if (null == reminder)
            {
                throw new InvalidCommandException(
                    $"Password reminder not found. Email={command.Email}, Code={command.ResetCode}"
                );
            }

            var customer = await _customerRepository.GetByEmailAsync(reminder.Email);
            var newPassword = _passwordHasher.HashPassword(command.NewPassword);

            customer.ResetPassword(reminder, newPassword);

            _passwordReminder.Remove(reminder);

            return Unit.Value;
        }
    }
}