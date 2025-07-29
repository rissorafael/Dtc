using Dtc.Domain.Entities;
using MediatR;

namespace Dtc.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}