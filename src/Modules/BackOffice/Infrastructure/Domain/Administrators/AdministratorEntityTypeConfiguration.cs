using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Domain.Administrators
{
    public class AdministratorEntityTypeConfiguration : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.ToTable("administrator", "backoffice");

            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<string>("_email").HasColumnName("email");
            builder.Property<string>("_password").HasColumnName("password");
            builder.Property<string>("_firstName").HasColumnName("first_name");
            builder.Property<string>("_lastName").HasColumnName("last_name");
            builder.Property<string>("_role").HasColumnName("role");
            builder.Property<DateTime>("_createdAt").HasColumnName("created_at");
        }
    }
}