using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.BuildingBlocks.Infrastructure.DomainEvents
{
    public class EntityFrameworkDomainEventsProvider : IDomainEventsProvider
    {
        private readonly DbContext _context;

        public EntityFrameworkDomainEventsProvider(DbContext context)
        {
            _context = context;
        }

        public List<IDomainEvent> GetAllDomainEvents()
        {
            return GetDomainEntities()
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = GetDomainEntities();

            foreach (var entry in domainEntities)
                entry.Entity.ClearDomainEvents();
        }

        private IEnumerable<EntityEntry<Entity>> GetDomainEntities()
        {
            return _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();
        }
    }
}