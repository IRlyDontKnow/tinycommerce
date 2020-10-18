using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Catalog.Domain.Categories;

namespace TinyCommerce.Modules.Catalog.Application.Categories
{
    public class CategoryCounter : ICategoryCounter
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CategoryCounter(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int CountByName(string name, CategoryId parentId)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT(name) FROM catalog.category WHERE name = @name AND parent_id = @parentId";

            return connection.ExecuteScalar<int>(sql, new {name, parentId = parentId?.Value});
        }

        public int CountBySlug(string slug, CategoryId parentId)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT(slug) FROM catalog.category WHERE slug = @slug AND parent_id = @parentId";

            return connection.ExecuteScalar<int>(sql, new {slug, parentId = parentId?.Value});
        }
    }
}