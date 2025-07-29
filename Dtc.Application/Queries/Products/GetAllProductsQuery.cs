using Dtc.Domain.Entities;
using MediatR;

namespace Dtc.Application.Queries.Products
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }
}
