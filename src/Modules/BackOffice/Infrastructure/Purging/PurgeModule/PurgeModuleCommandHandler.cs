using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.BuildingBlocks.Infrastructure.DataAccess;
using TinyCommerce.Modules.BackOffice.Application.Configuration;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Purging.PurgeModule
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
                "backoffice.administrator",
                "backoffice.outbox_messages"
            });

            return Unit.Value;
        }
    }
}