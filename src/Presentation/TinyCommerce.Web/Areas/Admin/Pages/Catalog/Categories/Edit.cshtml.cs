using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TinyCommerce.Modules.Catalog.Application.Categories.EditCategory;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategoriesForSelect;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategory;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Web.Areas.Admin.Pages.Catalog.Categories
{
    public class Edit : PageModel
    {
        private readonly ICatalogModule _catalogModule;

        public Edit(ICatalogModule catalogModule)
        {
            _catalogModule = catalogModule;
        }

        [FromRoute]
        public Guid CategoryId { get; set; }

        [BindProperty]
        public EditCategoryRequest FormData { get; set; }

        public List<SelectListItem> ParentCategoryOptions { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var category = await _catalogModule.ExecuteQueryAsync(new GetCategoryQuery(CategoryId));

            if (category == null)
                return NotFound();

            await LoadParentCategories(new List<Guid> {category.Id});

            FormData = new EditCategoryRequest
            {
                Slug = category.Slug,
                Name = category.Name,
                Description = category.Description,
                ParentId = category.ParentId
            };

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var category = await _catalogModule.ExecuteQueryAsync(new GetCategoryQuery(CategoryId));

            if (category == null)
                return NotFound();

            await _catalogModule.ExecuteCommandAsync(new EditCategoryCommand(
                CategoryId,
                FormData.Slug,
                FormData.Name,
                FormData.Description,
                FormData.ParentId
            ));

            return Page(); // TODO: redirect
        }

        // TODO: Add support for edit

        private async Task LoadParentCategories(List<Guid> excludedCategories = default)
        {
            var parentCategories = await _catalogModule.ExecuteQueryAsync(
                new GetCategoriesForSelectQuery(excludedCategories)
            );
            ParentCategoryOptions = new List<SelectListItem>
            {
                new SelectListItem(null, null)
            };

            ParentCategoryOptions.AddRange(parentCategories.Select(
                x => new SelectListItem(x.Path, x.Id.ToString())
            ));
        }
    }

    public class EditCategoryRequest
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class EditCategoryRequestValidator : AbstractValidator<EditCategoryRequest>
    {
        public EditCategoryRequestValidator()
        {
            RuleFor(x => x.Slug).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}