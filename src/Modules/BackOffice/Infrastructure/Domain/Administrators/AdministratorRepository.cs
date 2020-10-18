using System.Threading.Tasks;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Domain.Administrators
{
    internal class AdministratorRepository : IAdministratorRepository
    {
        private readonly BackOfficeContext _context;

        public AdministratorRepository(BackOfficeContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Administrator administrator)
        {
            await _context.Administrators.AddAsync(administrator);
        }
    }
}