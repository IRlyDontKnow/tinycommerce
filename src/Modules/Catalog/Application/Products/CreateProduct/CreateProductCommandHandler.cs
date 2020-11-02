using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.Catalog.Application.Configuration;
using TinyCommerce.Modules.Catalog.Domain.Products;

namespace TinyCommerce.Modules.Catalog.Application.Products.CreateProduct
{
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}