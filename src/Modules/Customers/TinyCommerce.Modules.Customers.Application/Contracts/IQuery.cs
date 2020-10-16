using MediatR;

namespace TinyCommerce.Modules.Customers.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}