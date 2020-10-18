using System.Threading.Tasks;

namespace TinyCommerce.Modules.BackOffice.Domain.Administrators
{
    public interface IAdministratorRepository
    {
        Task AddAsync(Administrator administrator);
    }
}