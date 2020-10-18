using System.Threading.Tasks;
using Autofac;
using MediatR;
using TinyCommerce.Modules.BackOffice.Application.Authentication;
using TinyCommerce.Modules.BackOffice.Application.Contracts;
using TinyCommerce.Modules.BackOffice.Infrastructure.Configuration;

namespace TinyCommerce.Modules.BackOffice.Infrastructure
{
    public class BackOfficeModule : IBackOfficeModule
    {
        public async Task ExecuteCommandAsync(ICommand command)
        {
            var scope = BackOfficeCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            await mediator.Send(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            var scope = BackOfficeCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            return await mediator.Send(query);
        }

        public Task<AuthenticationResult> AuthenticateAdministrator(string email, string password)
        {
            var scope = BackOfficeCompositionRoot.BeginLifetimeScope();
            var authenticator = scope.Resolve<IAuthenticator>();

            return authenticator.AuthenticateAsync(email, password);
        }
    }
}