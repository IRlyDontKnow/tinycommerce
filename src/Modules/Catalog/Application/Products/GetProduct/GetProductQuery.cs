using System;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Products.GetProduct
{
    public class GetProductQuery : IQuery<ProductDto>
    {
        public GetProductQuery(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; }
    }
}
