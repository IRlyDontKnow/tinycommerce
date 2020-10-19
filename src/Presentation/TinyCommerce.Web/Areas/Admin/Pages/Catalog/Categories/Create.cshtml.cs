using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TinyCommerce.Modules.Catalog.Application.Categories.CreateCategory;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategoriesForSelect;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Web.Areas.Admin.Pages.Catalog.Categories
{
    public class Create : PageModel
    {
        private readonly ICatalogModule _catalogModule;

        public Create(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        [BindProperty]
        public CreateCategoryRequest FormData { get; set; } = new CreateCategoryRequest();

        [FromRoute]
        public Guid? ParentId { get; set; } = null;

        public List<SelectListItem> ParentCategoryOptions { get; set; }

        public async Task OnGet()
        {
            FormData.ParentId = ParentId;

            await LoadParentCategories();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var categoryId = Guid.NewGuid();

            await _catalogModule.ExecuteCommandAsync(new CreateCategoryCommand(
                categoryId,
                FormData.Slug,
                FormData.Name,
                FormData.Description,
                FormData.ParentId
            ));

            return RedirectToPage("/Catalog/Categories/Edit", new
            {
                area = "Admin",
                categoryId = categoryId.ToString()
            });
        }

        private async Task LoadParentCategories()
        {
            var parentCategories = await _catalogModule.ExecuteQueryAsync(new GetCategoriesForSelectQuery());
            ParentCategoryOptions = new List<SelectListItem>
            {
                new SelectListItem(null, null)
            };

            ParentCategoryOptions.AddRange(parentCategories.Select(
                x => new SelectListItem(x.Path, x.Id.ToString())
            ));
        }
    }

    public class CreateCategoryRequest
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.Slug).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}