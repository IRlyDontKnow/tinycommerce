using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Catalog.Application.Configuration;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategory
{
    internal class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetCategoryQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<CategoryDto> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    id AS {nameof(CategoryDto.Id)},
                    slug AS {nameof(CategoryDto.Slug)},
                    name AS {nameof(CategoryDto.Name)},
                    description AS {nameof(CategoryDto.Description)},
                    parent_id AS {nameof(CategoryDto.ParentId)},
                    created_at AS {nameof(CategoryDto.CreatedAt)},
                    updated_at AS {nameof(CategoryDto.UpdatedAt)}
                FROM catalog.category
                WHERE id = @CategoryId
            ";

            return await connection.QueryFirstOrDefaultAsync<CategoryDto>(sql, new {query.CategoryId});
        }
    }
}