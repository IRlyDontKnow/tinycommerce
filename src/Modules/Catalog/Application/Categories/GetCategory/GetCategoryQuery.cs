using System;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategory
{
    public class GetCategoryQuery : IQuery<CategoryDto>
    {
        public GetCategoryQuery(Guid? categoryId = null, string slug = null)
        {
            if (!categoryId.HasValue && string.IsNullOrEmpty(slug))
            {
                throw new ArgumentException("You must provide categoryId or slug.");
            }

            if (categoryId.HasValue && !string.IsNullOrEmpty(slug))
            {
                throw new ArgumentException("You must provide either categoryId or slug.");
            }

            CategoryId = categoryId;
            Slug = slug;
        }

        public Guid? CategoryId { get; }

        public string Slug { get; }
    }
}