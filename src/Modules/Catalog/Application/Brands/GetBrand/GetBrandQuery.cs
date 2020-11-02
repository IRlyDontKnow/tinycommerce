using System;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Brands.GetBrand
{
    public class GetBrandQuery : IQuery<BrandDto>
    {
        public GetBrandQuery(Guid brandId)
        {
            BrandId = brandId;
        }

        public Guid BrandId { get; }
    }
}
