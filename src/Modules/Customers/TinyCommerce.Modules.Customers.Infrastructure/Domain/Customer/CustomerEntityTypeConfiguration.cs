using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CustomerDomainModel = TinyCommerce.Modules.Customers.Domain.Customers.Customer;

namespace TinyCommerce.Modules.Customers.Infrastructure.Domain.Customer
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<CustomerDomainModel>
    {
        public void Configure(EntityTypeBuilder<CustomerDomainModel> builder)
        {
            builder.ToTable("customer", "customers");

            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<string>("_email").HasColumnName("email");
            builder.Property<string>("_password").HasColumnName("password");
            builder.Property<string>("_firstName").HasColumnName("first_name");
            builder.Property<string>("_lastName").HasColumnName("last_name");
            builder.Property<DateTime>("_registrationDate").HasColumnName("registration_date");
        }
    }
}