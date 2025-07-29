using Dtc.Application.Queries.Products;
using Dtc.Domain.Entities;
using Dtc.Domain.Interfaces;
using MediatR;

namespace Dtc.Application.Handlers.Products
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IProductsRepository _productsRepository;

        public GetAllProductsQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productsRepository.GetAllAsync();
        }
    }
}