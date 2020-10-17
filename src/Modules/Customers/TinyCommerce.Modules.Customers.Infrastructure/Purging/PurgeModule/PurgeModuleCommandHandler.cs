using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.BuildingBlocks.Infrastructure.DataAccess;
using TinyCommerce.Modules.Customers.Application.Configuration;

namespace TinyCommerce.Modules.Customers.Infrastructure.Purging.PurgeModule
{
    public class PurgeModuleCommandHandler : ICommandHandler<PurgeModuleCommand>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PurgeModuleCommandHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Unit> Handle(PurgeModuleCommand command, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            await PurgeHelper.PurgeAsync(connection, new[]
            {
                "customers.customer_registration",
                "customers.customer",
                "customers.password_reminder",
                "customers.internal_commands",
                "customers.outbox_messages"
            });

            return Unit.Value;
        }
    }
}