using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations;

namespace TinyCommerce.Modules.Customers.Infrastructure.Domain.CustomerRegistrations
{
    internal class CustomerRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<CustomerRegistration>
    {
        public void Configure(EntityTypeBuilder<CustomerRegistration> builder)
        {
            builder.ToTable("customer_registration", "customers");

            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<string>("_email").HasColumnName("email");
            builder.Property<string>("_password").HasColumnName("password");
            builder.Property<string>("_firstName").HasColumnName("first_name");
            builder.Property<string>("_lastName").HasColumnName("last_name");
            builder.Property<string>("_activationCode").HasColumnName("activation_code");
            builder.Property<DateTime>("_registrationDate").HasColumnName("registration_date");
            builder.OwnsOne<CustomerRegistrationStatus>("_status",
                o => { o.Property(p => p.Value).HasColumnName("status"); });
        }
    }
}