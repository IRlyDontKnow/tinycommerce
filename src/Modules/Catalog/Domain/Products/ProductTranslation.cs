using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Products
{
    [Obsolete]
    public class ProductTranslation : ValueObject
    {
        private string _locale;
        private string _name;
        private string _slug;
        private string _description;
    }
}
