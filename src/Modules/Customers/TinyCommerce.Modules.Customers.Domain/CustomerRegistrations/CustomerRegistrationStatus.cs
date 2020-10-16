using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations
{
    public class CustomerRegistrationStatus : ValueObject
    {
        public CustomerRegistrationStatus(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
        
        public static CustomerRegistrationStatus WaitingForConfirmation => new CustomerRegistrationStatus(nameof(WaitingForConfirmation));
        public static CustomerRegistrationStatus Confirmed => new CustomerRegistrationStatus(nameof(Confirmed));
        public static CustomerRegistrationStatus Expired => new CustomerRegistrationStatus(nameof(Expired));
    }
}