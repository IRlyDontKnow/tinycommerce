using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace TinyCommerce.BuildingBlocks.Infrastructure.DataAccess
{
    public static class PurgeHelper
    {
        public static async Task PurgeAsync(IDbConnection connection, params string[] tableNames)
        {
            var query = tableNames
                .Select(name => $"DELETE FROM {name};")
                .Aggregate((a, b) => $"{a}{b}");

            await connection.ExecuteAsync(query);
        }
    }
}