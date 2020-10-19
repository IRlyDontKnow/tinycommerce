using System;
using System.Collections.Generic;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategory;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategories
{
    public class GetCategoriesQuery : IQuery<List<CategoryDto>>
    {
        public GetCategoriesQuery(Guid? parentId = null)
        {
            ParentId = parentId;
        }

        public Guid? ParentId { get; }
    }
}
