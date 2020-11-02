using System;
using TinyCommerce.Modules.Catalog.Domain.Brands;

namespace TinyCommerce.Modules.Catalog.Tests.Unit.Domain.Brands
{
    public static class BrandSampleData
    {
        public static readonly BrandId Id = new BrandId(Guid.NewGuid());
        public const string Name = "Adidas";
        public const string Slug = "adidas";
        public const string Description = "Lorem ipsum";
    }
}