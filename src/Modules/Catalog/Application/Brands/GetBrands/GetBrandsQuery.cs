using TinyCommerce.BuildingBlocks.Application.Queries;
using TinyCommerce.Modules.Catalog.Application.Brands.GetBrand;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Brands.GetBrands
{
    public class GetBrandsQuery : IQuery<PagedResult<BrandDto>>, IPagedQuery
    {
        public GetBrandsQuery(int page, int perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        public int Page { get; }
        
        public int PerPage { get; }
    }
}