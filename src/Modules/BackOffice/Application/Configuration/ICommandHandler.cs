using MediatR;
using TinyCommerce.Modules.BackOffice.Application.Contracts;

namespace TinyCommerce.Modules.BackOffice.Application.Configuration
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}