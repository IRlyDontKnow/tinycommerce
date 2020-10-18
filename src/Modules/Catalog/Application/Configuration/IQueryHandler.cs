using MediatR;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Configuration
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}