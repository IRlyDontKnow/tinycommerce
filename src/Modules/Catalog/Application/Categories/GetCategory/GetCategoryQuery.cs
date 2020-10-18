using System;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategory
{
    public class GetCategoryQuery : IQuery<CategoryDto>
    {
        public GetCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; }
    }
}