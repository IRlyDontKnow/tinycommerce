using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCommerce.BuildingBlocks.Infrastructure.InternalCommands;

namespace TinyCommerce.Modules.Customers.Infrastructure.InternalCommands
{
    public class InternalCommandEntityTypeConfiguration : IEntityTypeConfiguration<InternalCommand>
    {
        public void Configure(EntityTypeBuilder<InternalCommand> builder)
        {
            builder.ToTable("internal_commands", "customers");

            builder.HasKey(b => b.Id).HasName("id");
            builder.Property(b => b.Id).ValueGeneratedNever().HasColumnName("id");
            builder.Property(b => b.Type).HasColumnName("type");
            builder.Property(b => b.Data).HasColumnName("data");
            builder.Property(b => b.ProcessedDate).HasColumnName("processed_date");
        }
    }
}