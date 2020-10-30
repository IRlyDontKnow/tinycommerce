using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategoryTree;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Web.ViewComponents
{
    [ViewComponent]
    public class HeaderCategoriesViewComponent : ViewComponent
    {
        private readonly ICatalogModule _catalogModule;

        public HeaderCategoriesViewComponent(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rootCategories = await _catalogModule.ExecuteQueryAsync(new GetCategoryTreeQuery());

            return View(new HeaderCategoriesViewModel
            {
                RootCategories = rootCategories
            });
        }
    }

    public class HeaderCategoriesViewModel
    {
        public List<CategoryTreeItemDto> RootCategories { get; set; }
    }
}
