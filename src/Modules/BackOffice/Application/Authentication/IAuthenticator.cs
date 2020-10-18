using System.Threading.Tasks;

namespace TinyCommerce.Modules.BackOffice.Application.Authentication
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> AuthenticateAsync(string email, string password);
    }
}