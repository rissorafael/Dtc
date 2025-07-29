using MongoDB.Driver;
using Dtc.Domain.Interfaces;
using Dtc.Domain.Entities;

namespace Dtc.Service.Products
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<ProductMongo> _collection;

        public ProductService(IConnectionFactory connectionFactory)
        {
            var database = connectionFactory.GetMongoDatabase();
            _collection = database.GetCollection<ProductMongo>("Products");
        }

        public async Task<List<ProductMongo>> GetAllAsync()
        {
            return await _collection.Find(p => true).ToListAsync();
        }

        public async Task<ProductMongo?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProductMongo> CreateAsync(ProductMongo product)
        {
            await _collection.InsertOneAsync(product);
            return product;
        }

        public async Task UpdateAsync(string id, ProductMongo product)
        {
            await _collection.ReplaceOneAsync(p => p.Id == id, product);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(p => p.Id == id);
        }
    }
}
