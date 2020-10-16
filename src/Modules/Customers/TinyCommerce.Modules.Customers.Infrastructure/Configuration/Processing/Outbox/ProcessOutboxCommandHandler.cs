using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.BuildingBlocks.Application.Events;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing.Outbox
{
    public class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IMediator _mediator;

        public ProcessOutboxCommandHandler(IDbConnectionFactory connectionFactory, IMediator mediator)
        {
            _connectionFactory = connectionFactory;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ProcessOutboxCommand command, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    _message.id AS {nameof(OutboxMessageDto.Id)},
                    _message.type AS {nameof(OutboxMessageDto.Type)},
                    _message.data AS {nameof(OutboxMessageDto.Data)}
                FROM customers.outbox_messages AS _message
                WHERE _message.processed_date IS NULL
            ";
            var messages = (await connection.QueryAsync<OutboxMessageDto>(sql)).AsList();

            foreach (var message in messages)
            {
                var type = typeof(ICustomersModule).Assembly.GetType(message.Type);

                if (type == null)
                {
                    throw new NullReferenceException($"Could not find class for type '{message.Type}'.");
                }

                var domainEventNotification =
                    JsonConvert.DeserializeObject(message.Data, type) as IDomainEventNotification;

                if (null == domainEventNotification)
                {
                    throw new ApplicationException(
                        $"Failed to deserialize json data into object of type '{message.Type}'.");
                }

                await _mediator.Publish(domainEventNotification, cancellationToken);
                await MarkCommandAsProcessed(connection, message.Id);
            }

            return Unit.Value;
        }

        private static async Task MarkCommandAsProcessed(IDbConnection connection, Guid commandId)
        {
            const string sql = @"
                UPDATE customers.outbox_messages
                SET processed_date = @Date
                WHERE id = @Id
            ";

            await connection.ExecuteAsync(sql, new
            {
                Id = commandId,
                Date = DateTime.UtcNow
            });
        }
    }
}