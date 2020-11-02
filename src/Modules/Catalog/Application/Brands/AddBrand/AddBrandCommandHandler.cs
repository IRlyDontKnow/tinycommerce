using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Catalog.Application.Configuration;
using TinyCommerce.Modules.Catalog.Domain.Brands;

namespace TinyCommerce.Modules.Catalog.Application.Brands.AddBrand
{
    internal class AddBrandCommandHandler : ICommandHandler<AddBrandCommand>
    {
        private readonly IBrandRepository _brandRepository;

        public AddBrandCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Unit> Handle(AddBrandCommand command, CancellationToken cancellationToken)
        {
            var brand = Brand.CreateNew(
                new BrandId(command.BrandId),
                command.Name,
                command.Slug,
                command.Description
            );

            await _brandRepository.AddAsync(brand);

            return Unit.Value;
        }
    }
}