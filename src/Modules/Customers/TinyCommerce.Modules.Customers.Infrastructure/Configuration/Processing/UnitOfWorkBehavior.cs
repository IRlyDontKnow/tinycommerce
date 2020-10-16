using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TinyCommerce.BuildingBlocks.Infrastructure;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomersContext _context;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork, CustomersContext context)
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

            if (command is InternalCommandBase)
            {
                var internalCommand = await _context.InternalCommands.FirstOrDefaultAsync(
                    x => x.Id == command.Id, cancellationToken
                );

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return response;
        }
    }
}