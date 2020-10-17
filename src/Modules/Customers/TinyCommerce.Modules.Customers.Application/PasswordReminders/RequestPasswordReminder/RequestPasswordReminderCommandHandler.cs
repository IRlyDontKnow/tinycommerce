using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;

namespace TinyCommerce.Modules.Customers.Application.PasswordReminders.RequestPasswordReminder
{
    public class RequestPasswordReminderCommandHandler : ICommandHandler<RequestPasswordReminderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordReminderRepository _passwordReminderRepository;
        private readonly IResetCodeGenerator _resetCodeGenerator;

        public RequestPasswordReminderCommandHandler(
            ICustomerRepository customerRepository,
            IPasswordReminderRepository passwordReminderRepository,
            IResetCodeGenerator resetCodeGenerator
        )
        {
            _customerRepository = customerRepository;
            _passwordReminderRepository = passwordReminderRepository;
            _resetCodeGenerator = resetCodeGenerator;
        }

        public async Task<Unit> Handle(RequestPasswordReminderCommand command, CancellationToken cancellationToken)
        {
            var reminder = await _passwordReminderRepository.GetByEmailAsync(command.Email);

            if (null != reminder)
            {
                _passwordReminderRepository.Remove(reminder);
            }

            var customer = await _customerRepository.GetByEmailAsync(command.Email);

            if (customer == null)
                return Unit.Value;

            var passwordReminder = customer.RemindPassword(_resetCodeGenerator);

            await _passwordReminderRepository.AddAsync(passwordReminder);

            return Unit.Value;
        }
    }
}