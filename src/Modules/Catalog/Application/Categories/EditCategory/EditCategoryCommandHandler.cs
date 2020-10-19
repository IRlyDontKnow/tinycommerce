using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.BuildingBlocks.Application;
using TinyCommerce.Modules.Catalog.Application.Configuration;
using TinyCommerce.Modules.Catalog.Domain.Categories;

namespace TinyCommerce.Modules.Catalog.Application.Categories.EditCategory
{
    internal class EditCategoryCommandHandler : ICommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryCounter _categoryCounter;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryCounter categoryCounter)
        {
            _categoryRepository = categoryRepository;
            _categoryCounter = categoryCounter;
        }

        public async Task<Unit> Handle(EditCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(new CategoryId(command.CategoryId));

            if (category == null)
                throw new InvalidCommandException($"Category with id '{command.CategoryId}' not found.");

            Category parentCategory = null;

            if (command.ParentId.HasValue)
            {
                parentCategory = await _categoryRepository.GetByIdAsync(new CategoryId(command.ParentId.Value));

                if (null == parentCategory)
                {
                    throw new InvalidCommandException(
                        $"Could not find parent category with id '{command.ParentId.Value}'.");
                }
            }

            category.Edit(command.Slug, command.Name, command.Description, parentCategory?.Id, _categoryCounter);

            return Unit.Value;
        }
    }
}