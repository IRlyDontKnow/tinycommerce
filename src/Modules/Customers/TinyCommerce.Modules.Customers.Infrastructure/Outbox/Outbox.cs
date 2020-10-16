using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;

namespace TinyCommerce.Modules.Customers.Infrastructure.Outbox
{
    internal class Outbox : IOutbox
    {
        private readonly CustomersContext _context;

        public Outbox(CustomersContext context)
        {
            _context = context;
        }

        public void Add(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
        }
    }
}