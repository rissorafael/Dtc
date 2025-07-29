using Dtc.Application.Queries.Products;
using Dtc.Domain.Entities;
using Dtc.Domain.Interfaces;
using MediatR;

namespace Dtc.Application.Handlers.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IProductsRepository _repository;

        public GetProductByIdHandler(IProductsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}