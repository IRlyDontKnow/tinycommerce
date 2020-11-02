using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Products
{
    public class ProductSeo : ValueObject
    {
        public ProductSeo(string title, string description, string keywords)
        {
            Title = title;
            Description = description;
            Keywords = keywords;
        }

        public string Title { get; }

        public string Description { get; }

        public string Keywords { get; }
    }
}