using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Outbox
{
    internal class Outbox : IOutbox
    {
        private readonly CatalogContext _context;

        public Outbox(CatalogContext context)
        {
            _context = context;
        }

        public void Add(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
        }
    }
}