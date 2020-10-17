using Microsoft.EntityFrameworkCore;
using TinyCommerce.BuildingBlocks.Infrastructure.InternalCommands;
using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;
using TinyCommerce.Modules.Customers.Domain.CustomerRegistrations;
using TinyCommerce.Modules.Customers.Domain.Customers;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;
using TinyCommerce.Modules.Customers.Infrastructure.Domain.Customer;
using TinyCommerce.Modules.Customers.Infrastructure.Domain.CustomerRegistrations;
using TinyCommerce.Modules.Customers.Infrastructure.Domain.PasswordReminders;
using TinyCommerce.Modules.Customers.Infrastructure.InternalCommands;
using TinyCommerce.Modules.Customers.Infrastructure.Outbox;

namespace TinyCommerce.Modules.Customers.Infrastructure
{
    internal class CustomersContext : DbContext
    {
        internal DbSet<CustomerRegistration> CustomerRegistrations { get; set; }
        internal DbSet<Customer> Customers { get; set; }
        internal DbSet<PasswordReminder> PasswordReminders { get; set; }
        internal DbSet<OutboxMessage> OutboxMessages { get; set; }
        internal DbSet<InternalCommand> InternalCommands { get; set; }

        public CustomersContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerRegistrationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordReminderEntityTypeConfiguration());
        }
    }
}