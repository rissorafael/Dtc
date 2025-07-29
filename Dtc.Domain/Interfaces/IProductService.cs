using Dtc.Domain.Entities;

namespace Dtc.Domain.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductMongo>> GetAllAsync();
        Task<ProductMongo?> GetByIdAsync(string id);
        Task<ProductMongo> CreateAsync(ProductMongo product);
        Task UpdateAsync(string id, ProductMongo product);
        Task DeleteAsync(string id);
    }
}
