using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Customers.Application.Configuration;

namespace TinyCommerce.Modules.Customers.Application.Customers.GetCustomer
{
    public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetCustomerQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<CustomerDto> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    id AS {nameof(CustomerDto.Id)},
                    email AS {nameof(CustomerDto.Email)},
                    password AS {nameof(CustomerDto.Password)},
                    first_name AS {nameof(CustomerDto.FirstName)},
                    last_name AS {nameof(CustomerDto.LastName)},
                    registration_date AS {nameof(CustomerDto.RegistrationDate)}
                FROM customers.customer
                WHERE id = @CustomerId
            ";

            return await connection.QueryFirstOrDefaultAsync<CustomerDto>(sql, new {query.CustomerId});
        }
    }
}