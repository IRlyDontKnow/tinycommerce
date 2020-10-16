using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Customers.Application.Authentication;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations;
using TinyCommerce.Modules.Customers.Domain.Customers;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.RegisterNewCustomer
{
    public class RegisterNewCustomerCommandHandler : ICommandHandler<RegisterNewCustomerCommand>
    {
        private readonly ICustomerRegistrationRepository _customerRegistrationRepository;
        private readonly ICustomerChecker _customerChecker;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterNewCustomerCommandHandler(
            ICustomerRegistrationRepository customerRegistrationRepository,
            ICustomerChecker customerChecker,
            IPasswordHasher passwordHasher
        )
        {
            _customerRegistrationRepository = customerRegistrationRepository;
            _customerChecker = customerChecker;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(RegisterNewCustomerCommand command, CancellationToken cancellationToken)
        {
            var customerRegistration = CustomerRegistration.RegisterNewCustomer(
                new CustomerRegistrationId(command.CustomerRegistrationId),
                command.Email,
                _passwordHasher.HashPassword(command.Password),
                command.FirstName,
                command.LastName,
                _customerChecker
            );

            await _customerRegistrationRepository.AddAsync(customerRegistration);

            return Unit.Value;
        }
    }
}