using System;

namespace TinyCommerce.BuildingBlocks.EventBus
{
    public class IntegrationEvent
    {
        public Guid Id { get; }
        
        public DateTime OccurredOn { get; }

        public IntegrationEvent(Guid id, DateTime occurredOn)
        {
            Id = id;
            OccurredOn = occurredOn;
        }
    }
}