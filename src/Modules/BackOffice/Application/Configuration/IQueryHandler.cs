using MediatR;
using TinyCommerce.Modules.BackOffice.Application.Contracts;

namespace TinyCommerce.Modules.BackOffice.Application.Configuration
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}