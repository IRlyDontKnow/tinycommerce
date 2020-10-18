using System.Threading.Tasks;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Infrastructure
{
    public class CatalogModule : ICatalogModule
    {
        public async Task ExecuteCommandAsync(ICommand command)
        {
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            return default;
        }
    }
}