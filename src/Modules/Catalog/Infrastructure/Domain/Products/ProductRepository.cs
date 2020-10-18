using System;
using System.Threading.Tasks;
using TinyCommerce.Modules.Catalog.Domain.Products;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Domain.Products
{
    internal class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Catalog.Domain.Products.Product product)
        {
            throw new NotImplementedException();
            // await _context.Products.AddAsync(product);
        }
    }
}