using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Domain.Products
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Catalog.Domain.Products.Product>
    {
        public void Configure(EntityTypeBuilder<Catalog.Domain.Products.Product> builder)
        {
            
        }
    }
}