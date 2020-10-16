using System.Collections.Generic;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.BuildingBlocks.Infrastructure.DomainEvents
{
    public interface IDomainEventsProvider
    {
        List<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}