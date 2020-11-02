using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Web.Pages.Catalog.Product
{
    public class Show : PageModel
    {
        private readonly ICatalogModule _catalogModule;

        public Show(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        public async Task<IActionResult> OnGet([FromRoute] string productSlug)
        {
            return Page();
        }
    }
}
