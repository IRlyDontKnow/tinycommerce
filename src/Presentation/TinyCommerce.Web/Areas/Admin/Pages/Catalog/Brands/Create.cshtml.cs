using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Catalog.Application.Brands.AddBrand;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Web.Areas.Admin.Pages.Catalog.Brands
{
    public class Create : PageModel
    {
        private readonly ICatalogModule _catalogModule;

        public Create(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        [BindProperty]
        public CreateBrandRequest FormData { get; set; } = new CreateBrandRequest();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var brandId = Guid.NewGuid();

            await _catalogModule.ExecuteCommandAsync(new AddBrandCommand(
                brandId,
                FormData.Name,
                FormData.Slug,
                FormData.Description
            ));

            // TODO: Display flash message
            
            return RedirectToPage("/Catalog/Brands/Edit", new
            {
                area = "Admin",
                brandId = brandId.ToString()
            });
        }
    }

    public class CreateBrandRequest
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }
    }

    public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
    {
        public CreateBrandRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Slug).NotEmpty();
        }
    }
}