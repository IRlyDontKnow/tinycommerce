using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.ConfirmRegistration
{
    public class ConfirmRegistrationCommandHandler : ICommandHandler<ConfirmRegistrationCommand>
    {
        private readonly ICustomerRegistrationRepository _customerRegistrationRepository;

        public ConfirmRegistrationCommandHandler(ICustomerRegistrationRepository customerRegistrationRepository)
        {
            _customerRegistrationRepository = customerRegistrationRepository;
        }

        public async Task<Unit> Handle(ConfirmRegistrationCommand command, CancellationToken cancellationToken)
        {
            var registration =
                await _customerRegistrationRepository.GetByIdAsync(
                    new CustomerRegistrationId(command.CustomerRegistrationId));

            registration.Confirm(command.ActivationCode);

            return Unit.Value;
        }
    }
}