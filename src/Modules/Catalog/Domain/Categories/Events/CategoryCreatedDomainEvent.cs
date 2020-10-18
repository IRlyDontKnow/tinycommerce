using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Categories.Events
{
    public class CategoryCreatedDomainEvent : DomainEventBase
    {
        public CategoryCreatedDomainEvent(
            CategoryId categoryId,
            string slug,
            string name,
            string description,
            CategoryId parentId,
            DateTime createdAt
        )
        {
            CategoryId = categoryId;
            Slug = slug;
            Name = name;
            Description = description;
            ParentId = parentId;
            CreatedAt = createdAt;
        }

        public CategoryId CategoryId { get; }
        public string Slug { get; }
        public string Name { get; }
        public string Description { get; }
        public CategoryId ParentId { get; }
        public DateTime CreatedAt { get; }
    }
}