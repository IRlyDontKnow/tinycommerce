using Microsoft.EntityFrameworkCore;
using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;
using TinyCommerce.Modules.Catalog.Domain.Categories;
using TinyCommerce.Modules.Catalog.Domain.Products;
using TinyCommerce.Modules.Catalog.Infrastructure.Domain.Categories;
using TinyCommerce.Modules.Catalog.Infrastructure.Outbox;

namespace TinyCommerce.Modules.Catalog.Infrastructure
{
    internal class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options) : base(options)
        {
        }

        internal DbSet<Category> Categories { get; set; }
        // internal DbSet<Product> Products { get; set; }
        internal DbSet<OutboxMessage> OutboxMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        }
    }
}