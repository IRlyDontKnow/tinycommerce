using System;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Catalog.Domain.Brands.Events;
using TinyCommerce.Modules.Catalog.Domain.SeedWork;

namespace TinyCommerce.Modules.Catalog.Domain.Brands
{
    public class Brand : Entity, IAggregateRoot
    {
        private Brand()
        {
            // Entity framework
        }

        public Brand(BrandId id, string name, string slug, string description)
        {
            // TODO: Make sure brand slug is unique

            Id = id;
            _name = name;
            _slug = slug;
            _description = description;
            _createdAt = SystemClock.Now;

            AddDomainEvent(new BrandCreatedDomainEvent(
                Id,
                _name,
                _slug,
                _description,
                _createdAt
                ));
        }

        public BrandId Id { get; }
        private string _name;
        private string _slug;
        private string _description;
        private DateTime _createdAt;

        public static Brand CreateNew(BrandId id, string name, string slug, string description)
        {
            return new Brand(id, name, slug, description);
        }
    }
}