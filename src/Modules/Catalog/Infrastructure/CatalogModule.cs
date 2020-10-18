using System.Threading.Tasks;
using Autofac;
using MediatR;
using TinyCommerce.Modules.Catalog.Application.Contracts;
using TinyCommerce.Modules.Catalog.Infrastructure.Configuration;

namespace TinyCommerce.Modules.Catalog.Infrastructure
{
    public class CatalogModule : ICatalogModule
    {
        public async Task ExecuteCommandAsync(ICommand command)
        {
            var scope = CatalogCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            await mediator.Send(command);
        }

        public Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            var scope = CatalogCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            return mediator.Send(query);
        }
    }
}
