using System.Threading.Tasks;
using TinyCommerce.Modules.Customers.Application.Authentication;

namespace TinyCommerce.Modules.Customers.Application.Contracts
{
    public interface ICustomersModule
    {
        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);

        Task<AuthenticationResult> Authenticate(string email, string password);
    }
}