using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Brands.Events
{
    public class BrandCreatedDomainEvent : DomainEventBase
    {
        public BrandCreatedDomainEvent(
            BrandId brandId,
            string name,
            string slug,
            string description,
            DateTime createdAt
        )
        {
            BrandId = brandId;
            Name = name;
            Slug = slug;
            Description = description;
            CreatedAt = createdAt;
        }

        public BrandId BrandId { get; }

        public string Name { get; }

        public string Slug { get; }

        public string Description { get; }

        public DateTime CreatedAt { get; }
    }
}
