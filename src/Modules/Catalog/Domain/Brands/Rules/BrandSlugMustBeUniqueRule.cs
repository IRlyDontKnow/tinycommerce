using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Brands.Rules
{
    public class BrandSlugMustBeUniqueRule : IBusinessRule
    {
        public bool IsBroken()
        {
            // TODO: Implement

            return false;
        }

        public string Message => "Brand slug must be unique.";
    }
}
