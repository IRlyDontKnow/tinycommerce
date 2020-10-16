using System.Threading.Tasks;

namespace TinyCommerce.Modules.Customers.Application.Authentication
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> AuthenticateAsync(string email, string password);
    }
}