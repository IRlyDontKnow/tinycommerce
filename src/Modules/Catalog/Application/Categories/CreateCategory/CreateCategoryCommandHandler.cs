using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.BuildingBlocks.Application;
using TinyCommerce.Modules.Catalog.Application.Configuration;
using TinyCommerce.Modules.Catalog.Domain.Categories;

namespace TinyCommerce.Modules.Catalog.Application.Categories.CreateCategory
{
    internal class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryCounter _categoryCounter;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryCounter categoryCounter)
        {
            _categoryRepository = categoryRepository;
            _categoryCounter = categoryCounter;
        }

        public async Task<Unit> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
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

            var category = Category.CreateNew(
                new CategoryId(command.CategoryId),
                command.Slug,
                command.Name,
                command.Description,
                parentCategory?.Id,
                _categoryCounter
            );

            await _categoryRepository.AddAsync(category);

            return Unit.Value;
        }
    }
}