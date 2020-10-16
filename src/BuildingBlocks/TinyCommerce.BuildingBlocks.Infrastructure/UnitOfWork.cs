using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinyCommerce.BuildingBlocks.Infrastructure.DomainEvents;

namespace TinyCommerce.BuildingBlocks.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(DbContext context, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _context = context;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchAsync();

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
