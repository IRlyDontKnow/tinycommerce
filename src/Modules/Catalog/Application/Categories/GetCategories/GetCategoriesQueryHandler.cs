using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategory;
using TinyCommerce.Modules.Catalog.Application.Configuration;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategories
{
    internal class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetCategoriesQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var builder = new SqlBuilder();
            var template = builder.AddTemplate($@"
                SELECT
                    id AS {nameof(CategoryDto.Id)},
                    slug AS {nameof(CategoryDto.Slug)},
                    name AS {nameof(CategoryDto.Name)},
                    description AS {nameof(CategoryDto.Description)},
                    parent_id AS {nameof(CategoryDto.ParentId)},
                    created_at AS {nameof(CategoryDto.CreatedAt)},
                    updated_at AS {nameof(CategoryDto.UpdatedAt)}
                FROM catalog.category
                /**where**/
            ");

            if (query.ParentId.HasValue)
                builder.Where("parent_id = @ParentId", new {ParentId = query.ParentId.Value});
            else
                builder.Where("parent_id IS NULL");

            var result = await connection.QueryAsync<CategoryDto>(template.RawSql, template.Parameters);

            return result.ToList();
        }
    }
}