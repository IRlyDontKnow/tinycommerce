using System;
using FluentValidation;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Categories.CreateCategory
{
    public class CreateCategoryCommand : CommandBase
    {
        public CreateCategoryCommand(Guid categoryId, string slug, string name, string description, Guid? parentId)
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

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.Slug).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}