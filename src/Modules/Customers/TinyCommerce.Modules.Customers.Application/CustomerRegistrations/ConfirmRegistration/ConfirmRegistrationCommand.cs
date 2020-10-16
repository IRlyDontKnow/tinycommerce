using System;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.ConfirmRegistration
{
    public class ConfirmRegistrationCommand : CommandBase
    {
        public ConfirmRegistrationCommand(Guid customerRegistrationId, string activationCode)
        {
            CustomerRegistrationId = customerRegistrationId;
            ActivationCode = activationCode;
        }

        public Guid CustomerRegistrationId { get; }
        public string ActivationCode { get; }
    }
}