using Dtc.Domain.Entities;
using MediatR;

namespace Dtc.Application.Commands.Products
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}