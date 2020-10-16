using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations
{
    public class ActivationCodeGenerator : IActivationCodeGenerator
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private static readonly Random Random = new Random();

        public ActivationCodeGenerator(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<string> GenerateAsync()
        {
            while (true)
            {
                var activationCode = RandomCode();

                if (await IsActivationCodeInUse(activationCode))
                {
                    continue;
                }


                return activationCode;
            }
        }

        private async Task<bool> IsActivationCodeInUse(string code)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT(id) FROM customers.customer_registration WHERE activation_code = @code;";

            return 0 < await connection.ExecuteScalarAsync<int>(sql, new {code});
        }

        private static string RandomCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}