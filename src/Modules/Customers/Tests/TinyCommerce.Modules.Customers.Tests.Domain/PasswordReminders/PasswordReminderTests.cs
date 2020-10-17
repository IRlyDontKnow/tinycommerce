using System;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders.Events;
using TinyCommerce.Modules.Customers.Domain.SeedWork;
using TinyCommerce.Modules.Customers.Tests.Domain.SeedWork;

namespace TinyCommerce.Modules.Customers.Tests.Domain.PasswordReminders
{
    [TestFixture]
    public class PasswordReminderTests : TestBase
    {
        [Test]
        public void CreatePasswordReminder_Test()
        {
            var id = new PasswordReminderId(Guid.NewGuid());
            var email = "john@squadore.com";
            var resetCodeGenerator = Substitute.For<IResetCodeGenerator>();
            resetCodeGenerator.Generate().Returns("KGI1VPO");

            var reminder = PasswordReminder.Create(id, email, resetCodeGenerator);
            var domainEvent = AssertPublishedDomainEvent<PasswordReminderCreatedDomainEvent>(reminder);

            resetCodeGenerator.Received(Quantity.Exactly(1)).Generate();
            Assert.That(domainEvent.PasswordReminderId, Is.EqualTo(id));
            Assert.That(domainEvent.Email, Is.EqualTo(email));
            Assert.That(domainEvent.Code, Is.EqualTo("KGI1VPO"));
        }

        [Test]
        public void HasPasswordReminderExpired_Test()
        {
            var resetCodeGenerator = Substitute.For<IResetCodeGenerator>();

            var reminder = PasswordReminder.Create(
                new PasswordReminderId(Guid.NewGuid()),
                "john@squadore.com",
                resetCodeGenerator
            );

            Assert.That(reminder.HasExpired, Is.False);

            SystemClock.Set(DateTime.Now.AddHours(49));
            Assert.That(reminder.HasExpired, Is.True);
        }
    }
}