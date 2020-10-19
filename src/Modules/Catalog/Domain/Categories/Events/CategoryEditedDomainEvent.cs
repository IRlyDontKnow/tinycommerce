using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Categories.Events
{
    public class CategoryEditedDomainEvent : DomainEventBase
    {
        public CategoryEditedDomainEvent(
            CategoryId categoryId,
            string slug,
            string name,
            string description,
            CategoryId parentId,
            DateTime updatedAt
        )
        {
            CategoryId = categoryId;
            Slug = slug;
            Name = name;
            Description = description;
            ParentId = parentId;
            UpdatedAt = updatedAt;
        }

        public CategoryId CategoryId { get; }
        public string Slug { get; }
        public string Name { get; }
        public string Description { get; }
        public CategoryId ParentId { get; }
        public DateTime UpdatedAt { get; }
    }
}