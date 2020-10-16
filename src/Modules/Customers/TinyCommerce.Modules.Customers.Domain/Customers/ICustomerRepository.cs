using System.Threading.Tasks;

namespace TinyCommerce.Modules.Customers.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);

        Task<Customer> GetByIdAsync(CustomerId id);
    }
}