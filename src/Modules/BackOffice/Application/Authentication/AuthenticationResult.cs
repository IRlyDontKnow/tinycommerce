namespace TinyCommerce.Modules.BackOffice.Application.Authentication
{
    public class AuthenticationResult
    {
        public AuthenticationResult(SecurityAdministrator administrator)
        {
            Authenticated = true;
            Administrator = administrator;
        }

        public AuthenticationResult(string error)
        {
            Error = error;
        }

        public SecurityAdministrator Administrator { get; }
        public bool Authenticated { get; }
        public string Error { get; }
    }
}
