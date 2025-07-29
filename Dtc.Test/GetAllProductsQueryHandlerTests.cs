using Dtc.Application.Handlers.Products;
using Dtc.Application.Queries.Products;
using Dtc.Domain.Entities;
using Dtc.Domain.Interfaces;
using Moq;

namespace Dtc.Test
{

    public class GetAllProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Pizza Calabresa", Price = 45.0M },
            new Product { Id = 2, Name = "Pizza Marguerita", Price = 40.0M }
        };

            var mockRepo = new Mock<IProductsRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            var handler = new GetAllProductsQueryHandler(mockRepo.Object);

            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Pizza Calabresa", result[0].Name);
            Assert.Equal(45.0M, result[0].Price);
        }
    }
}