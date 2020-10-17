using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;

namespace TinyCommerce.Modules.Customers.Infrastructure.Domain.PasswordReminders
{
    internal class PasswordReminderEntityTypeConfiguration : IEntityTypeConfiguration<PasswordReminder>
    {
        public void Configure(EntityTypeBuilder<PasswordReminder> builder)
        {
            builder.ToTable("password_reminder", "customers");

            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Code).HasColumnName("code");
            builder.Property<DateTime>("_requestedAt").HasColumnName("requested_at");
            builder.Property<DateTime>("_expirationDate").HasColumnName("expires_at");
        }
    }
}