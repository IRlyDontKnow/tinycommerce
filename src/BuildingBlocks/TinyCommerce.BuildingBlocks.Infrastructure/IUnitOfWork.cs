using System.Threading;
using System.Threading.Tasks;

namespace TinyCommerce.BuildingBlocks.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}