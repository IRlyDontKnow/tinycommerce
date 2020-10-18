using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Outbox
{
    public class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("outbox_messages", "catalog");

            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever();
            builder.Property(x => x.OccurredOn).HasColumnName("occurred_on");
            builder.Property(x => x.Type).HasColumnName("type");
            builder.Property(x => x.Data).HasColumnName("data");
            builder.Property(x => x.ProcessedDate).HasColumnName("processed_date");
        }
    }
}