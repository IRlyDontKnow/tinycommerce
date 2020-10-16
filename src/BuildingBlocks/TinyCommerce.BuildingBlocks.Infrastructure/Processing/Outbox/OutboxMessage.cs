using System;

namespace TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox
{
    public class OutboxMessage
    {
        public OutboxMessage(Guid id, DateTime occurredOn, string type, string data)
        {
            Id = Guid.NewGuid();
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
        }

        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }
}
