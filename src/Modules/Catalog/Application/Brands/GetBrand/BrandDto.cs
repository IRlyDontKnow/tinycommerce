using System;

namespace TinyCommerce.Modules.Catalog.Application.Brands.GetBrand
{
    public class BrandDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}