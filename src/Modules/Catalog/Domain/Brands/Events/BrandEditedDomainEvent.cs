using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Brands.Events
{
    public class BrandEditedDomainEvent : DomainEventBase
    {
        public BrandEditedDomainEvent(
            BrandId brandId,
            string name,
            string slug,
            string description,
            DateTime updatedAt
        )
        {
            BrandId = brandId;
            Name = name;
            Slug = slug;
            Description = description;
            UpdatedAt = updatedAt;
        }

        public BrandId BrandId { get; }

        public string Name { get; }

        public string Slug { get; }

        public string Description { get; }

        public DateTime UpdatedAt { get; }
    }
}