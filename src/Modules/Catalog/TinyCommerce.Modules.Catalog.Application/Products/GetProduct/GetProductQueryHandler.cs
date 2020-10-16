using System.Threading;
using System.Threading.Tasks;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.Modules.Catalog.Application.Configuration;

namespace TinyCommerce.Modules.Catalog.Application.Products.GetProduct
{
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetProductQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<ProductDto> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}