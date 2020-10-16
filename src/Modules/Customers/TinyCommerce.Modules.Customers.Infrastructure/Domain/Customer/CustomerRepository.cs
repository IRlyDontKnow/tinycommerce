using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinyCommerce.Modules.Customers.Domain.Customers;
using CustomerDomainModel = TinyCommerce.Modules.Customers.Domain.Customers.Customer;

namespace TinyCommerce.Modules.Customers.Infrastructure.Domain.Customer
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersContext _context;

        public CustomerRepository(CustomersContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CustomerDomainModel customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<CustomerDomainModel> GetByIdAsync(CustomerId id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}