using System;
using FluentValidation;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Application.Brands.EditBrand
{
    public class EditBrandCommand : CommandBase
    {
        public EditBrandCommand(Guid brandId, string name, string slug, string description)
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
    
    public class EditBrandCommandValidator : AbstractValidator<EditBrandCommand>
    {
        public EditBrandCommandValidator()
        {
            RuleFor(x => x.BrandId).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Slug).NotEmpty();
        }
    }
}