using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Catalog.Application.Configuration;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategoryTree
{
    internal class GetCategoryTreeQueryHandler : IQueryHandler<GetCategoryTreeQuery, List<CategoryTreeItemDto>>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetCategoryTreeQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<CategoryTreeItemDto>> Handle(GetCategoryTreeQuery query, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var builder = new SqlBuilder();
            var template = builder.AddTemplate($@"
                WITH RECURSIVE categoryTree AS (
                    SELECT id, name, slug, description, parent_id, 1 as level, name as path
                    FROM catalog.category
                    WHERE parent_id IS NULL
                    UNION ALL
                    SELECT c1.id, c1.name, c1.slug, c1.description, c1.parent_id, ct.level + 1, concat(ct.path, ' / ', c1.name)
                    FROM catalog.category c1,
                         categoryTree ct
                    WHERE c1.parent_id = ct.id
                )
                SELECT id          AS Id,
                       name        AS Name,
                       slug        AS Slug,
                       description AS Description,
                       level       AS Level,
                       path        AS Path,
                       parent_id   AS ParentId
                FROM categoryTree;
            ");

            var result = await connection.QueryAsync<CategoryTreeItemDto>(template.RawSql, template.Parameters);
            var categories = result.ToList();

            foreach (var category in categories)
            {
                category.Children = categories.Where(child => child.ParentId == category.Id).ToList();
            }
            
            return categories.Where(category => !category.ParentId.HasValue).ToList();
        }
    }
}
