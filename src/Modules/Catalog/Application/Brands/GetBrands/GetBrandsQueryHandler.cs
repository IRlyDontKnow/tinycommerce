using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.BuildingBlocks.Application.Queries;
using TinyCommerce.Modules.Catalog.Application.Brands.GetBrand;
using TinyCommerce.Modules.Catalog.Application.Configuration;

namespace TinyCommerce.Modules.Catalog.Application.Brands.GetBrands
{
    public class GetBrandsQueryHandler : IQueryHandler<GetBrandsQuery, PagedResult<BrandDto>>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetBrandsQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<PagedResult<BrandDto>> Handle(GetBrandsQuery query, CancellationToken cancellationToken)
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
                LIMIT @PerPage
                OFFSET @Offset;
                SELECT COUNT(id) FROM catalog.brand;
            ";

            var reader = await connection.QueryMultipleAsync(sql, new
            {
                query.PerPage,
                Offset = (query.Page - 1) * query.PerPage
            });

            var brands = await reader.ReadAsync<BrandDto>();
            var total = await reader.ReadFirstAsync<long>();

            return new PagedResult<BrandDto>(brands, total, query.Page, query.PerPage);
        }
    }
}
