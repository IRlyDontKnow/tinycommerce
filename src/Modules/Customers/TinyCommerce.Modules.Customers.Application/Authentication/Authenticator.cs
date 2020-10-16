using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;

namespace TinyCommerce.Modules.Customers.Application.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IPasswordHasher _passwordHasher;

        public Authenticator(IDbConnectionFactory connectionFactory, IPasswordHasher passwordHasher)
        {
            _connectionFactory = connectionFactory;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            var connection = _connectionFactory.GetOpenConnection();
            var customer = await FetchCustomer(connection, email);

            if (customer == null || !_passwordHasher.VerifyHashedPassword(customer.Password, password))
            {
                return new AuthenticationResult("Invalid credentials");
            }

            return new AuthenticationResult(customer);
        }

        private static async Task<AuthCustomerDto> FetchCustomer(IDbConnection connection, string email)
        {
            var sql = $@"
                SELECT
                    id AS {nameof(AuthCustomerDto.Id)},
                    email AS {nameof(AuthCustomerDto.Email)},
                    password AS {nameof(AuthCustomerDto.Password)},
                    first_name AS {nameof(AuthCustomerDto.FirstName)},
                    last_name AS {nameof(AuthCustomerDto.LastName)}
                FROM customers.customer
                WHERE email = @email
            ";

            return await connection.QueryFirstOrDefaultAsync<AuthCustomerDto>(sql, new {email});
        }
    }
}