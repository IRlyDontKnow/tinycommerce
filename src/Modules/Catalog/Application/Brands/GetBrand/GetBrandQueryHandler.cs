using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Catalog.Application.Configuration;

namespace TinyCommerce.Modules.Catalog.Application.Brands.GetBrand
{
    public class GetBrandQueryHandler : IQueryHandler<GetBrandQuery, BrandDto>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetBrandQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task<BrandDto> Handle(GetBrandQuery query, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    id AS {nameof(BrandDto.Id)},
                    name AS {nameof(BrandDto.Name)},
                    slug AS {nameof(BrandDto.Slug)},
                    description AS {nameof(BrandDto.Description)},
                    created_at AS {nameof(BrandDto.CreatedAt)}
                FROM catalog.brand
                WHERE id = @BrandId;
            ";

            return connection.QueryFirstOrDefaultAsync<BrandDto>(sql, new {query.BrandId});
        }
    }
}