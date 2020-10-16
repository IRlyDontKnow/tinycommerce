using System;
using System.Data;
using Npgsql;
using TinyCommerce.BuildingBlocks.Application.DataAccess;

namespace TinyCommerce.BuildingBlocks.Infrastructure.DataAccess
{
    public class PostgresConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public PostgresConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection?.State == ConnectionState.Open)
            {
                _connection?.Dispose();
            }
        }
    }
}