using System.Threading.Tasks;
using TinyCommerce.Modules.BackOffice.Application.Authentication;

namespace TinyCommerce.Modules.BackOffice.Application.Contracts
{
    public interface IBackOfficeModule
    {
        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);

        Task<AuthenticationResult> AuthenticateAdministrator(string email, string password);
    }
}