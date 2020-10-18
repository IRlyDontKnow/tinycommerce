using MediatR;

namespace TinyCommerce.Modules.BackOffice.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}