using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Events;
using TinyCommerce.Modules.Customers.Domain.Customers;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.ConfirmRegistration
{
    public class CustomerRegistrationConfirmedHandler : INotificationHandler<CustomerRegistrationConfirmedDomainEvent>
    {
        private readonly ICustomerRegistrationRepository _customerRegistrationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerChecker _customerChecker;

        public CustomerRegistrationConfirmedHandler(
            ICustomerRegistrationRepository customerRegistrationRepository,
            ICustomerRepository customerRepository,
            ICustomerChecker customerChecker
        )
        {
            _customerRegistrationRepository = customerRegistrationRepository;
            _customerRepository = customerRepository;
            _customerChecker = customerChecker;
        }

        public async Task Handle(CustomerRegistrationConfirmedDomainEvent @event, CancellationToken cancellationToken)
        {
            var registration = await _customerRegistrationRepository.GetByIdAsync(
                new CustomerRegistrationId(@event.CustomerRegistrationId.Value)
            );

            var customer = registration.CreateCustomer(_customerChecker);

            await _customerRepository.AddAsync(customer);
        }
    }
}
