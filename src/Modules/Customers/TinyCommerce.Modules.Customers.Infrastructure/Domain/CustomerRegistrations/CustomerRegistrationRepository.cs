using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations;

namespace TinyCommerce.Modules.Customers.Infrastructure.Domain.CustomerRegistrations
{
    internal class CustomerRegistrationRepository : ICustomerRegistrationRepository
    {
        private readonly CustomersContext _context;

        public CustomerRegistrationRepository(CustomersContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CustomerRegistration customerRegistration)
        {
            await _context.CustomerRegistrations.AddAsync(customerRegistration);
        }

        public async Task<CustomerRegistration> GetByIdAsync(CustomerRegistrationId id)
        {
            return await _context.CustomerRegistrations.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}