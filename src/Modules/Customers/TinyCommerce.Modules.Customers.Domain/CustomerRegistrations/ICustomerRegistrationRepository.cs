using System.Threading.Tasks;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations
{
    public interface ICustomerRegistrationRepository
    {
        Task AddAsync(CustomerRegistration customerRegistration);
        Task<CustomerRegistration> GetByIdAsync(CustomerRegistrationId id);
    }
}