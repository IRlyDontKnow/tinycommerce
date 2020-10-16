using Microsoft.EntityFrameworkCore;
using TinyCommerce.Modules.Catalog.Domain.Categories;
using TinyCommerce.Modules.Catalog.Domain.Products;

namespace TinyCommerce.Modules.Catalog.Infrastructure
{
    internal class CatalogContext : DbContext
    {
        internal DbSet<Product> Products { get; set; }
        
        internal DbSet<Category> Categories { get; set; }
        
        
    }
}