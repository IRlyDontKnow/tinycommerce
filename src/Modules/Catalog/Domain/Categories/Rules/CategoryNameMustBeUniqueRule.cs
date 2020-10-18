using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Categories.Rules
{
    public class CategoryNameMustBeUniqueRule : IBusinessRule
    {
        private readonly string _name;
        private readonly CategoryId _parentId;
        private readonly ICategoryCounter _categoryCounter;

        public CategoryNameMustBeUniqueRule(string name, CategoryId parentId, ICategoryCounter categoryCounter)
        {
            _name = name;
            _parentId = parentId;
            _categoryCounter = categoryCounter;
        }

        public bool IsBroken()
        {
            return _categoryCounter.CountByName(_name, _parentId) > 0;
        }

        public string Message => "Category name must be unique.";
    }
}