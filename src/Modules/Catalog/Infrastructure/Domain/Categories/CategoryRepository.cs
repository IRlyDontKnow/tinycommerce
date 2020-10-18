using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Category> GetByIdAsync(CategoryId id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}