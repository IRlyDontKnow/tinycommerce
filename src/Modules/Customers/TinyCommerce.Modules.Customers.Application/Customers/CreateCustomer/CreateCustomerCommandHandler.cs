using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Customers.Application.Authentication;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Domain.SeedWork;

namespace TinyCommerce.Modules.Customers.Application.Customers.CreateCustomer
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerChecker _customerChecker;
        private readonly IPasswordHasher _passwordHasher;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            ICustomerChecker customerChecker,
            IPasswordHasher passwordHasher
        )
        {
            _customerRepository = customerRepository;
            _customerChecker = customerChecker;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = Customer.CreateNew(
                new CustomerId(command.CustomerId),
                command.Email,
                _passwordHasher.HashPassword(command.Password),
                command.FirstName,
                command.LastName,
                SystemClock.Now,
                _customerChecker
            );

            await _customerRepository.AddAsync(customer);

            return Unit.Value;
        }
    }
}