using System;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Categories.EditCategory
{
    public class EditCategoryCommand : CommandBase
    {
        public EditCategoryCommand(Guid categoryId, string slug, string name, string description, Guid? parentId)
        {
            CategoryId = categoryId;
            Slug = slug;
            Name = name;
            Description = description;
            ParentId = parentId;
        }

        public Guid CategoryId { get; }
        public string Slug { get; }
        public string Name { get; }
        public string Description { get; }
        public Guid? ParentId { get; }
    }
}
