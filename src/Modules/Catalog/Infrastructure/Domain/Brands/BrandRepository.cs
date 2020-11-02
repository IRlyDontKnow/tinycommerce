using System.Threading.Tasks;
using TinyCommerce.Modules.Catalog.Domain.Brands;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Domain.Brands
{
    internal class BrandRepository : IBrandRepository
    {
        private readonly CatalogContext _context;

        public BrandRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
        }
    }
}