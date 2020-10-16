using System.Threading.Tasks;

namespace TinyCommerce.Modules.Catalog.Domain.Products
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
    }
}