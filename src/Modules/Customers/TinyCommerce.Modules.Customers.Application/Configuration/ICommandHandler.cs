using MediatR;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.Configuration
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}