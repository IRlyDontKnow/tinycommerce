namespace TinyCommerce.Modules.Customers.Application.Authentication
{
    public class AuthenticationResult
    {
        public AuthenticationResult(AuthCustomerDto customer)
        {
            Authenticated = true;
            Customer = customer;
        }

        public AuthenticationResult(string error)
        {
            Error = error;
        }

        public AuthCustomerDto Customer { get; }
        public bool Authenticated { get; }
        public string Error { get; }
    }
}