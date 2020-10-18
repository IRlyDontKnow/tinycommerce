using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Outbox
{
    internal class Outbox : IOutbox
    {
        private readonly BackOfficeContext _context;

        public Outbox(BackOfficeContext context)
        {
            _context = context;
        }

        public void Add(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
        }
    }
}