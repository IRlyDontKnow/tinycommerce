using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Catalog.Application.Configuration;
using TinyCommerce.Modules.Catalog.Domain.Brands;

namespace TinyCommerce.Modules.Catalog.Application.Brands.EditBrand
{
    public class EditBrandCommandHandler : ICommandHandler<EditBrandCommand>
    {
        private readonly IBrandRepository _brandRepository;

        public EditBrandCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Unit> Handle(EditBrandCommand command, CancellationToken cancellationToken)
        {
            // TODO:
            
            throw new System.NotImplementedException();
        }
    }
}