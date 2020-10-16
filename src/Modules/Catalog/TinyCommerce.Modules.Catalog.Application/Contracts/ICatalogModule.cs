using System.Threading.Tasks;

namespace TinyCommerce.Modules.Catalog.Application.Contracts
{
    public interface ICatalogModule
    {
        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}