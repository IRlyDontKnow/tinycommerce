using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Catalog.Application.Configuration;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategoriesForSelect
{
    internal class
        GetCategoriesForSelectQueryHandler : IQueryHandler<GetCategoriesForSelectQuery, List<CategoryItemDto>>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetCategoriesForSelectQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<CategoryItemDto>> Handle(
            GetCategoriesForSelectQuery query,
            CancellationToken cancellationToken
        )
        {
            var connection = _connectionFactory.GetOpenConnection();
            var builder = new SqlBuilder();
            var template = builder.AddTemplate($@"
                WITH RECURSIVE categoryTree AS (
                        SELECT id, name, parent_id, 1 as level, name as path
                        FROM catalog.category
                        WHERE parent_id IS NULL
                    UNION ALL
                        SELECT c1.id, c1.name, c1.parent_id, ct.level + 1, concat(ct.path, ' / ', c1.name)
                        FROM catalog.category c1, categoryTree ct
                        /**where**/
                )
                SELECT 
                    id AS {nameof(CategoryItemDto.Id)},
                    name AS {nameof(CategoryItemDto.Name)},
                    path AS {nameof(CategoryItemDto.Path)},
                    parent_id AS {nameof(CategoryItemDto.ParentId)}
                FROM categoryTree;
            ");

            // TODO: Exclude root categories too!

            builder.Where("c1.parent_id = ct.id");

            if (query.ExcludedCategories.Any())
            {
                builder.Where("c1.id != ANY(@ExcludedCategories)", new {query.ExcludedCategories});
            }

            var result = await connection.QueryAsync<CategoryItemDto>(template.RawSql, template.Parameters);

            return result.ToList();
        }
    }
}