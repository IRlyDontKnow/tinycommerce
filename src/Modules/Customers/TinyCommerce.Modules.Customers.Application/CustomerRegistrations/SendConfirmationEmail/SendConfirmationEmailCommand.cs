using System;
using Newtonsoft.Json;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.SendConfirmationEmail
{
    public class SendConfirmationEmailCommand : InternalCommandBase
    {
        [JsonConstructor]
        public SendConfirmationEmailCommand(Guid id, Guid customerRegistrationId)
            : base(id)
        {
            CustomerRegistrationId = customerRegistrationId;
        }

        public Guid CustomerRegistrationId { get; }
    }
}