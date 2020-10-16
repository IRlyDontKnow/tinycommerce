using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using MediatR;
using Newtonsoft.Json;
using TinyCommerce.BuildingBlocks.Application.Events;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;
using TinyCommerce.BuildingBlocks.Infrastructure.Serialization;

namespace TinyCommerce.BuildingBlocks.Infrastructure.DomainEvents
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _scope;
        private readonly IOutbox _outbox;
        private readonly IDomainEventsProvider _domainEventsProvider;

        public DomainEventsDispatcher(
            IMediator mediator,
            ILifetimeScope scope,
            IOutbox outbox,
            IDomainEventsProvider domainEventsProvider
        )
        {
            _mediator = mediator;
            _scope = scope;
            _outbox = outbox;
            _domainEventsProvider = domainEventsProvider;
        }

        public async Task DispatchAsync()
        {
            var domainEvents = _domainEventsProvider.GetAllDomainEvents();
            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();

            foreach (var domainEvent in domainEvents)
            {
                var domainEventNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType =
                    domainEventNotificationType.MakeGenericType(domainEvent.GetType());
                var domainEventNotification = _scope.ResolveOptional(domainNotificationWithGenericType,
                    new List<Parameter>
                    {
                        new NamedParameter("domainEvent", domainEvent),
                        new NamedParameter("id", domainEvent.Id)
                    });

                if (domainEventNotification != null)
                    domainEventNotifications.Add(domainEventNotification as IDomainEventNotification<IDomainEvent>);
            }

            _domainEventsProvider.ClearAllDomainEvents();

            var tasks = domainEvents.Select(async (domainEvent) => { await _mediator.Publish(domainEvent); });

            await Task.WhenAll(tasks);

            foreach (var domainEventNotification in domainEventNotifications)
            {
                var type = domainEventNotification.GetType().FullName;
                var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });

                var outboxMessage = new OutboxMessage(
                    domainEventNotification.Id,
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data
                );

                _outbox.Add(outboxMessage);
            }
        }
    }
}