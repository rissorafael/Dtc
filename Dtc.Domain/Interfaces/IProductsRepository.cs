using Dtc.Domain.Entities;

namespace Dtc.Domain.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task CreateAsync(Product produto);
        Task UpdateAsync(Product produto);
        Task RemoveAsync(int id);
    }
}
