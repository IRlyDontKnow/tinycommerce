using System;
using System.Collections.Generic;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategoriesForSelect
{
    public class GetCategoriesForSelectQuery : IQuery<List<CategoryItemDto>>
    {
        public GetCategoriesForSelectQuery(List<Guid> excludedCategories = default)
        {
            ExcludedCategories = excludedCategories;
        }

        public List<Guid> ExcludedCategories { get; }
    }
}