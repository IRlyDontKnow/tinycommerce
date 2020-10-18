using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCommerce.Modules.Catalog.Domain.Categories;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Domain.Categories
{
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category", "catalog");

            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<string>("_slug").HasColumnName("slug");
            builder.Property<string>("_name").HasColumnName("name");
            builder.Property<string>("_description").HasColumnName("description");
            builder.Property<CategoryId>("_parentId").HasColumnName("parent_id");
            builder.Property<DateTime>("_createdAt").HasColumnName("created_at");
            builder.Property<DateTime?>("_updatedAt").HasColumnName("updated_at");
        }
    }
}