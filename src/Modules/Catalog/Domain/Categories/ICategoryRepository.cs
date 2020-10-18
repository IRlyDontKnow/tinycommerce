using System.Threading.Tasks;

namespace TinyCommerce.Modules.Catalog.Domain.Categories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
    }
}