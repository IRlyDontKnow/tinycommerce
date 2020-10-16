using MediatR;

namespace TinyCommerce.Modules.Catalog.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}