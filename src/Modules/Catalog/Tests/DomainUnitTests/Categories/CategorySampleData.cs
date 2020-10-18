using System;
using TinyCommerce.Modules.Catalog.Domain.Categories;

namespace TinyCommerce.Modules.Catalog.Tests.Unit.Domain.Categories
{
    public static class CategorySampleData
    {
        public static readonly CategoryId Id = new CategoryId(Guid.NewGuid());
        public const string Slug = "home-accessories";
        public const string Name = "Home Accessories";
        public const string Description = "Lorem ipsum";
        public static readonly CategoryId ParentId = new CategoryId(Guid.NewGuid());
    }
}