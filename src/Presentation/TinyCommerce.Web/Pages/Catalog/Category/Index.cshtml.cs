using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategory;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Web.Pages.Catalog.Category
{
    public class Index : PageModel
    {
        private readonly ICatalogModule _catalogModule;

        public Index(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        public CategoryDto Category { get; private set; }

        public async Task<IActionResult> OnGet([FromRoute] string slug)
        {
            Category = await _catalogModule.ExecuteQueryAsync(new GetCategoryQuery(slug: slug));

            if (Category == null)
            {
                return RedirectToPage("/Errors/NotFound");
            }

            return Page();
        }
    }
}
