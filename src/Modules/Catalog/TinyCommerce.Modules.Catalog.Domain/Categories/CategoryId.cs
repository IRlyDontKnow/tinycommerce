using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Categories
{
    public class CategoryId : TypedIdValueBase
    {
        public CategoryId(Guid value) : base(value)
        {
        }
    }
}