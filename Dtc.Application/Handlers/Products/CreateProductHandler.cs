using System.Text.Json;
using Dct.Infra.CrossCutting.Kafka;
using Dtc.Application.Commands.Products;
using Dtc.Domain.Entities;
using Dtc.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Dtc.Application.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductsRepository _repository;
        private readonly KafkaProducer _kafkaProducer;
        private readonly ILogger<CreateProductHandler> _logger;

        public CreateProductHandler(IProductsRepository repository, KafkaProducer kafkaProducer, ILogger<CreateProductHandler> logger)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
            _logger = logger;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            await _repository.CreateAsync(product);

            var jsonProduto = JsonSerializer.Serialize(product);
            await _kafkaProducer.SendMessageAsync(jsonProduto);

            _logger.LogInformation("Produto criado e enviado ao Kafka: {ProdutoId}", product.Id);

            return product;
        }
    }
}
