using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Products
{
    public class ProductTranslation : ValueObject
    {
        private string _language;
        private string _name;
        private string _slug;
        private string _description;
        private string _shortDescription;
        private string _seoKeywords;
        private string _seoDescription;
    }
}