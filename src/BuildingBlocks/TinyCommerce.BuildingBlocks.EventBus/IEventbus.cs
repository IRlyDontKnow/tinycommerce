namespace TinyCommerce.BuildingBlocks.EventBus
{
    public interface IEventbus
    {
        void Publish<T>(T @event) where T : IntegrationEvent;

        void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent;
    }
}