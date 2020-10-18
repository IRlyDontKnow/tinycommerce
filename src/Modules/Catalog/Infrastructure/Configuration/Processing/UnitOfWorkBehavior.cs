using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.BuildingBlocks.Infrastructure;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CatalogContext _context;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork, CatalogContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<TResponse> Handle(
            TRequest command,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next
        )
        {
            var response = await next();

            await _unitOfWork.CommitAsync(cancellationToken);

            return response;
        }
    }
}