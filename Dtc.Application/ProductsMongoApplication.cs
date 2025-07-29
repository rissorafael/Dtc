using Dtc.Domain.Entities;
using Dtc.Domain.Interfaces;

namespace DTC.Application
{
    public class ProductsMongoApplication : IProductsMongoApplication
    {
        private readonly IProductService _productService;

        public ProductsMongoApplication(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<ProductMongo>> GetAllAsync()
        {
            return await _productService.GetAllAsync();
        }

        public async Task<ProductMongo?> GetByIdAsync(string id)
        {
            return await _productService.GetByIdAsync(id);
        }

        public async Task<ProductMongo> CreateAsync(ProductMongo product)
        {
            return await _productService.CreateAsync(product);
        }

        public async Task UpdateAsync(string id, ProductMongo product)
        {
            await _productService.UpdateAsync(id, product);
        }

        public async Task DeleteAsync(string id)
        {
            await _productService.DeleteAsync(id);
        }
    }
}
