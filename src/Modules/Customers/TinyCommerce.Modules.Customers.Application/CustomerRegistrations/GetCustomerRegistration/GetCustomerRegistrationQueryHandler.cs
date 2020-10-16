using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Customers.Application.Configuration;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.GetCustomerRegistration
{
    public class
        GetCustomerRegistrationQueryHandler : IQueryHandler<GetCustomerRegistrationQuery, CustomerRegistrationDto>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetCustomerRegistrationQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<CustomerRegistrationDto> Handle(
            GetCustomerRegistrationQuery query,
            CancellationToken cancellationToken
        )
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    id AS {nameof(CustomerRegistrationDto.Id)},
                    email AS {nameof(CustomerRegistrationDto.Email)},
                    password AS {nameof(CustomerRegistrationDto.Password)},
                    first_name AS {nameof(CustomerRegistrationDto.FirstName)},
                    last_name AS {nameof(CustomerRegistrationDto.LastName)},
                    activation_code As {nameof(CustomerRegistrationDto.ActivationCode)},
                    registration_date As {nameof(CustomerRegistrationDto.RegistrationDate)}
                FROM customers.customer_registration
                WHERE id = @CustomerRegistrationId
            ";

            return await connection.QueryFirstOrDefaultAsync<CustomerRegistrationDto>(
                sql,
                new
                {
                    query.CustomerRegistrationId
                }
            );
        }
    }
}