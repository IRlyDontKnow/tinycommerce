using MediatR;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Configuration
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}