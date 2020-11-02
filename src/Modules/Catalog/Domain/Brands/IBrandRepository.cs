using System.Threading.Tasks;

namespace TinyCommerce.Modules.Catalog.Domain.Brands
{
    public interface IBrandRepository
    {
        Task AddAsync(Brand brand);
    }
}