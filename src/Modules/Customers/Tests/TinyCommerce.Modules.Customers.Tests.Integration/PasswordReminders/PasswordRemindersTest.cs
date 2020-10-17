using System;
using System.Threading.Tasks;
using Dapper;
using NUnit.Framework;
using TinyCommerce.Modules.Customers.Application.Customers.CreateCustomer;
using TinyCommerce.Modules.Customers.Application.Customers.GetCustomer;
using TinyCommerce.Modules.Customers.Application.PasswordReminders;
using TinyCommerce.Modules.Customers.Application.PasswordReminders.RequestPasswordReminder;
using TinyCommerce.Modules.Customers.Application.PasswordReminders.ResetPassword;
using TinyCommerce.Modules.Customers.Tests.Integration.Customers;
using TinyCommerce.Modules.Customers.Tests.Integration.SeedWork;
using TinyCommerce.Modules.Customers.Domain.SeedWork;
using TinyCommerce.Modules.Customers.Infrastructure.PasswordReminders.CleanupExpiredPasswordReminders;

namespace TinyCommerce.Modules.Customers.Tests.Integration.PasswordReminders
{
    [TestFixture]
    public class PasswordRemindersTest : TestBase
    {
        [Test]
        public async Task ResetCustomerPassword_Lifecycle_Test()
        {
            await CreateSampleCustomer();

            ResetCodeGenerator.SetCustomCode("KU7AXR");
            await CustomersModule.ExecuteCommandAsync(new RequestPasswordReminderCommand(
                CustomerSampleData.Email
            ));

            var oldPassword = (await CustomersModule.ExecuteQueryAsync(
                new GetCustomerQuery(CustomerSampleData.Id)
            ))?.Password;

            await CustomersModule.ExecuteCommandAsync(new ResetPasswordCommand(
                CustomerSampleData.Email,
                "KU7AXR",
                "Password123"
            ));

            var customer = await CustomersModule.ExecuteQueryAsync(
                new GetCustomerQuery(CustomerSampleData.Id)
            );

            Assert.That(customer.Password, Is.Not.EqualTo(oldPassword));
        }

        [Test]
        public async Task CleanupExpiredReminders_Test()
        {
            await CreateSampleCustomerWithEmail("john1@squadore.com");
            await CustomersModule.ExecuteCommandAsync(new RequestPasswordReminderCommand(
                "john1@squadore.com"
            ));

            SystemClock.Set(DateTime.Now.AddDays(-5));

            await CreateSampleCustomerWithEmail("john2@squadore.com");
            await CustomersModule.ExecuteCommandAsync(new RequestPasswordReminderCommand(
                "john2@squadore.com"
            ));

            await CreateSampleCustomerWithEmail("john3@squadore.com");
            await CustomersModule.ExecuteCommandAsync(new RequestPasswordReminderCommand(
                "john3@squadore.com"
            ));

            await CustomersModule.ExecuteCommandAsync(new CleanupExpiredPasswordRemindersCommand());

            var connection = ConnectionFactory.GetOpenConnection();
            var count = await connection.ExecuteScalarAsync<int>("SELECT COUNT(id) FROM customers.password_reminder");

            Assert.That(count, Is.EqualTo(1));
        }

        private async Task CreateSampleCustomer()
        {
            await CustomersModule.ExecuteCommandAsync(new CreateCustomerCommand(
                CustomerSampleData.Id,
                CustomerSampleData.Email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName
            ));
        }

        private async Task CreateSampleCustomerWithEmail(string email)
        {
            await CustomersModule.ExecuteCommandAsync(new CreateCustomerCommand(
                Guid.NewGuid(),
                email,
                CustomerSampleData.Password,
                CustomerSampleData.FirstName,
                CustomerSampleData.LastName
            ));
        }
    }
}