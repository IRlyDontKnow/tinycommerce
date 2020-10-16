using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using Polly;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing.InternalCommands
{
    public class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProcessInternalCommandsCommandHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    id AS {nameof(InternalCommandDto.Id)},
                    type AS {nameof(InternalCommandDto.Type)},
                    data AS {nameof(InternalCommandDto.Data)}
                FROM customers.internal_commands
                WHERE processed_date IS NULL
            ";

            var internalCommands = (await connection.QueryAsync<InternalCommandDto>(sql)).ToList();
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });

            foreach (var internalCommand in internalCommands)
            {
                var result = await retryPolicy.ExecuteAndCaptureAsync(() => ProcessCommand(internalCommand));

                if (result.Outcome == OutcomeType.Failure)
                {
                    await MarkCommandAsFailed(connection, internalCommand, result.FinalException.ToString());
                }
            }

            return Unit.Value;
        }

        private static async Task ProcessCommand(InternalCommandDto internalCommand)
        {
            var type = typeof(ICustomersModule).Assembly.GetType(internalCommand.Type);

            if (null == type)
            {
                throw new ArgumentNullException(nameof(type));
            }

            dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);
            await CommandsExecutor.Execute(commandToProcess);
        }

        private static async Task MarkCommandAsFailed(
            IDbConnection connection,
            InternalCommandDto internalCommand,
            string error
        )
        {
            const string sql = @"
                UPDATE customers.internal_commands
                SET processed_date = @ProcessedDate, error = @Error
                WHERE id = @Id
            ";

            await connection.ExecuteAsync(sql, new
            {
                internalCommand.Id,
                ProcessedDate = DateTime.UtcNow,
                Error = error
            });
        }
    }
}