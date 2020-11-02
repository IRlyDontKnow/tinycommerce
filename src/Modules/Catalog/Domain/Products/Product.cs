using System.Collections.Generic;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Catalog.Domain.Brands;

namespace TinyCommerce.Modules.Catalog.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        private Product()
        {
            // Entity framework
        }

        public ProductId Id { get; }
        private string _name;
        private string _slug;
        private string _code;
        private string _shortDescription;
        private string _fullDescription;
        // private List<ProductImage> _images;
        private BrandId _brandId;
    }
}
