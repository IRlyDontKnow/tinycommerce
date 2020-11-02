using System;
using FluentValidation;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Brands.AddBrand
{
    public class AddBrandCommand : CommandBase
    {
        public AddBrandCommand(Guid brandId, string name, string slug, string description)
        {
            BrandId = brandId;
            Name = name;
            Slug = slug;
            Description = description;
        }

        public Guid BrandId { get; }

        public string Name { get; }

        public string Slug { get; }

        public string Description { get; }
    }

    public class AddBrandCommandValidator : AbstractValidator<AddBrandCommand>
    {
        public AddBrandCommandValidator()
        {
            RuleFor(x => x.BrandId).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Slug).NotEmpty();
        }
    }
}