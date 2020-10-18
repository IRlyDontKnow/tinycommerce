using Microsoft.EntityFrameworkCore;
using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;
using TinyCommerce.Modules.BackOffice.Infrastructure.Domain.Administrators;
using TinyCommerce.Modules.BackOffice.Infrastructure.Outbox;

namespace TinyCommerce.Modules.BackOffice.Infrastructure
{
    internal class BackOfficeContext : DbContext
    {
        public BackOfficeContext(DbContextOptions options) : base(options)
        {
        }

        internal DbSet<Administrator> Administrators { get; set; }
        internal DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AdministratorEntityTypeConfiguration());
        }
    }
}