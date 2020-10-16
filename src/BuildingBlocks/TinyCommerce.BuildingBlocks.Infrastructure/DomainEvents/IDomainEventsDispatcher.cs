using System.Threading.Tasks;

namespace TinyCommerce.BuildingBlocks.Infrastructure.DomainEvents
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchAsync();
    }
}