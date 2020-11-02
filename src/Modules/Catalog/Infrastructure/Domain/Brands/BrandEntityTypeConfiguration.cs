using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCommerce.Modules.Catalog.Domain.Brands;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Domain.Brands
{
    public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("brand", "catalog");

            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<string>("_name").HasColumnName("name");
            builder.Property<string>("_slug").HasColumnName("slug");
            builder.Property<string>("_description").HasColumnName("description");
            builder.Property<DateTime>("_createdAt").HasColumnName("created_at");
        }
    }
}