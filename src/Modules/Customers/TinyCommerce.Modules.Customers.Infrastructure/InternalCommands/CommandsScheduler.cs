using System;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.BuildingBlocks.Infrastructure.Serialization;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Infrastructure.InternalCommands
{
    internal class CommandsScheduler : ICommandsScheduler
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CommandsScheduler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task EnqueueAsync(ICommand command)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sql = @"
                INSERT INTO customers.internal_commands (id, type, data, enqueue_date)
                VALUES (@Id, @Type, @Data, @EnqueueDate)
            ";

            await connection.ExecuteScalarAsync(sql, new
            {
                command.Id,
                Type = command.GetType().FullName,
                EnqueueDate = DateTime.UtcNow,
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                })
            });
        }
    }
}