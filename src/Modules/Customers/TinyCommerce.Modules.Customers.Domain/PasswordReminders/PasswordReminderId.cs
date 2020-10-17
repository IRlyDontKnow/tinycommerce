using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.PasswordReminders
{
    public class PasswordReminderId : TypedIdValueBase
    {
        public PasswordReminderId(Guid value) : base(value)
        {
        }
    }
}