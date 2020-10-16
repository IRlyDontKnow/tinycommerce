using System.Threading.Tasks;
using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TinyCommerce.Modules.Customers.Application.Authentication;
using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration;

namespace TinyCommerce.Modules.Customers.Infrastructure
{
    public class CustomersModule : ICustomersModule
    {
        public async Task ExecuteCommandAsync(ICommand command)
        {
            await using var scope = CustomersCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            await using var scope = CustomersCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(query);
        }

        public async Task<AuthenticationResult> Authenticate(string email, string password)
        {
            await using var scope = CustomersCompositionRoot.BeginLifetimeScope();
            var authenticator = scope.Resolve<IAuthenticator>();

            return await authenticator.AuthenticateAsync(email, password);
        }
    }
}