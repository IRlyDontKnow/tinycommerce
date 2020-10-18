using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Categories.Rules
{
    public class CategorySlugMustBeUniqueRule : IBusinessRule
    {
        private readonly string _slug;
        private readonly CategoryId _parentId;
        private readonly ICategoryCounter _categoryCounter;

        public CategorySlugMustBeUniqueRule(string slug, CategoryId parentId, ICategoryCounter categoryCounter)
        {
            _slug = slug;
            _parentId = parentId;
            _categoryCounter = categoryCounter;
        }

        public bool IsBroken()
        {
            return _categoryCounter.CountBySlug(_slug, _parentId) > 0;
        }

        public string Message => "Category slug must be unique.";
    }
}