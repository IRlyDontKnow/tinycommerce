using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Customers.Domain.Customers;

namespace TinyCommerce.Modules.Customers.Application.Customers
{
    public class CustomerChecker : ICustomerChecker
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CustomerChecker(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool IsCustomerEmailInUse(string email)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var count = connection.ExecuteScalar<int>(
                "SELECT COUNT(id) FROM customers.customer WHERE email = @email",
                new {email}
            );

            return count > 0;
        }
    }
}