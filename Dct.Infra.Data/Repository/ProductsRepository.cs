using Dct.Infra.Data.Context;
using Dtc.Domain.Entities;
using Dtc.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dct.Infra.Data.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsDbContext _context;

        public ProductsRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task CreateAsync(Product produto)
        {
            await _context.Products.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product produto)
        {
            _context.Products.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var produto = await _context.Products.FindAsync(id);
            if (produto != null)
            {
                _context.Products.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}