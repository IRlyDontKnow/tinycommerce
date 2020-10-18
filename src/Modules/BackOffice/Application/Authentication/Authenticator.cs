using System;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;

namespace TinyCommerce.Modules.BackOffice.Application.Authentication
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

            var administrator = await FetchAdministrator(email);

            if (administrator == null || !_passwordHasher.VerifyHashedPassword(administrator.Password, password))
            {
                return new AuthenticationResult("Invalid credentials");
            }

            return new AuthenticationResult(administrator);
        }

        private async Task<SecurityAdministrator> FetchAdministrator(string email)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    id AS {nameof(SecurityAdministrator.Id)},
                    password AS {nameof(SecurityAdministrator.Password)},
                    email AS {nameof(SecurityAdministrator.Email)},
                    first_name AS {nameof(SecurityAdministrator.FirstName)},
                    last_name AS {nameof(SecurityAdministrator.LastName)},
                    role AS {nameof(SecurityAdministrator.Role)}
                FROM backoffice.administrator
                WHERE email = @email
            ";

            return await connection.QueryFirstOrDefaultAsync<SecurityAdministrator>(sql, new {email});
        }
    }
}