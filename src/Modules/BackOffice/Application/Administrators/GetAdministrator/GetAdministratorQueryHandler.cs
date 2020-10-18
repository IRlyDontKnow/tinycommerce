using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.BackOffice.Application.Configuration;

namespace TinyCommerce.Modules.BackOffice.Application.Administrators.GetAdministrator
{
    internal class GetAdministratorQueryHandler : IQueryHandler<GetAdministratorQuery, AdministratorDto>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetAdministratorQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<AdministratorDto> Handle(GetAdministratorQuery query, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    id AS {nameof(AdministratorDto.Id)},
                    password AS {nameof(AdministratorDto.Password)},
                    email AS {nameof(AdministratorDto.Email)},
                    first_name AS {nameof(AdministratorDto.FirstName)},
                    last_name AS {nameof(AdministratorDto.LastName)},
                    role AS {nameof(AdministratorDto.Role)},
                    created_at AS {nameof(AdministratorDto.CreatedAt)}
                FROM backoffice.administrator
                WHERE id = @AdministratorId
            ";

            return await connection.QueryFirstOrDefaultAsync<AdministratorDto>(sql, new {query.AdministratorId});
        }
    }
}