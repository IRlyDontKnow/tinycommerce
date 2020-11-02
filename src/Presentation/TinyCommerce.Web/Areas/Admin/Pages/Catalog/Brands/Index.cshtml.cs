using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.BuildingBlocks.Application.Queries;
using TinyCommerce.Modules.Catalog.Application.Brands.GetBrand;
using TinyCommerce.Modules.Catalog.Application.Brands.GetBrands;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Web.Areas.Admin.Pages.Catalog.Brands
{
    public class Index : PageModel
    {
        private readonly ICatalogModule _catalogModule;

        public Index(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        public PagedResult<BrandDto> Brands { get; private set; }

        [FromRoute(Name = "Page")]
        public int Page { get; set; } = 1;

        [FromRoute(Name = "PerPage")]
        public int PerPage { get; set; } = 15;

        public async Task OnGet()
        {
            Brands = await _catalogModule.ExecuteQueryAsync(new GetBrandsQuery(Page, PerPage));
        }
    }
}