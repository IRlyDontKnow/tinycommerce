using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;

namespace TinyCommerce.Modules.BackOffice.Application.Administrators
{
    public class AdministratorCounter : IAdministratorCounter
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AdministratorCounter(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int CountByEmail(string email)
        {
            const string sql = "SELECT COUNT(id) FROM backoffice.administrator WHERE email = @email";

            return _connectionFactory
                .GetOpenConnection()
                .ExecuteScalar<int>(sql, new {email});
        }
    }
}