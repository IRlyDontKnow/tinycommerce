using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategories;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategory;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Web.Areas.Admin.Pages.Catalog.Categories
{
    public class Index : PageModel
    {
        private readonly ICatalogModule _catalogModule;

        public Index(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        public List<CategoryDto> Categories { get; private set; }

        [FromRoute]
        public Guid? ParentId { get; set; }

        public async Task OnGet()
        {
            Categories = await _catalogModule.ExecuteQueryAsync(new GetCategoriesQuery(ParentId));
        }
    }
}