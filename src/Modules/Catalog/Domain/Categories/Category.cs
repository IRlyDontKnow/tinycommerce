using System;
using TinyCommerce.BuildingBlocks.Domain;
using TinyCommerce.Modules.Catalog.Domain.Categories.Events;
using TinyCommerce.Modules.Catalog.Domain.Categories.Rules;
using TinyCommerce.Modules.Catalog.Domain.SeedWork;

namespace TinyCommerce.Modules.Catalog.Domain.Categories
{
    public class Category : Entity, IAggregateRoot
    {
        private Category()
        {
            // Entity framework
        }

        private Category(
            CategoryId id,
            string slug,
            string name,
            string description,
            CategoryId parentId,
            ICategoryCounter categoryCounter
        )
        {
            CheckRule(new CategorySlugMustBeUniqueRule(slug, parentId, categoryCounter));
            CheckRule(new CategoryNameMustBeUniqueRule(name, parentId, categoryCounter));

            Id = id;
            _slug = slug;
            _name = name;
            _description = description;
            _parentId = parentId;
            _createdAt = SystemClock.Now;
            _updatedAt = null;

            AddDomainEvent(new CategoryCreatedDomainEvent(
                Id,
                _slug,
                _name,
                _description,
                _parentId,
                _createdAt
            ));
        }

        public CategoryId Id { get; }
        private string _slug;
        private string _name;
        private string _description;
        private CategoryId _parentId;
        private DateTime _createdAt;
        private DateTime? _updatedAt;

        public void Edit(
            string slug,
            string name,
            string description,
            CategoryId parentId,
            ICategoryCounter categoryCounter
        )
        {
            // TODO: Add test
            
            if (slug != _slug)
                CheckRule(new CategorySlugMustBeUniqueRule(slug, parentId, categoryCounter));

            if (name != _name)
                CheckRule(new CategoryNameMustBeUniqueRule(name, parentId, categoryCounter));

            _slug = slug;
            _name = name;
            _description = description;
            _parentId = parentId;
            _updatedAt = SystemClock.Now;

            AddDomainEvent(new CategoryEditedDomainEvent(
                Id,
                _slug,
                _name,
                _description,
                _parentId,
                _updatedAt.Value
            ));
        }

        public static Category CreateNew(
            CategoryId id,
            string slug,
            string name,
            string description,
            CategoryId parentId,
            ICategoryCounter categoryCounter
        )
        {
            return new Category(id, slug, name, description, parentId, categoryCounter);
        }
    }
}