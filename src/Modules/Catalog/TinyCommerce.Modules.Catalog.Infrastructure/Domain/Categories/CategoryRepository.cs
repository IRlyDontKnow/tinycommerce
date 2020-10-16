using System.Threading.Tasks;
using TinyCommerce.Modules.Catalog.Domain.Categories;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Domain.Categories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogContext _context;

        public CategoryRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }
    }
}