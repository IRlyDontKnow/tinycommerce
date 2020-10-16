namespace TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox
{
    public interface IOutbox
    {
        void Add(OutboxMessage message);
    }
}
