using System.Threading.Tasks;
using Autofac;
using MediatR;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration
{
    internal static class CommandsExecutor
    {
        public static async Task Execute(ICommand command)
        {
            await using var scope = CustomersCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }
    }
}