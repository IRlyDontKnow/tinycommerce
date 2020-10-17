using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Customers.Application.Configuration;

namespace TinyCommerce.Modules.Customers.Infrastructure.PasswordReminders.CleanupExpiredPasswordReminders
{
    public class CleanupExpiredPasswordRemindersCommandHandler : ICommandHandler<CleanupExpiredPasswordRemindersCommand>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CleanupExpiredPasswordRemindersCommandHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Unit> Handle(
            CleanupExpiredPasswordRemindersCommand command,
            CancellationToken cancellationToken
        )
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sql = "DELETE FROM customers.password_reminder WHERE expires_at < @NowDate";

            await connection.ExecuteAsync(sql, new {NowDate = DateTime.UtcNow});

            return Unit.Value;
        }
    }
}