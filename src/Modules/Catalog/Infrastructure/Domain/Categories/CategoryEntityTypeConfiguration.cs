using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCommerce.Modules.Catalog.Domain.Categories;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Domain.Categories
{
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            
        }
    }
}