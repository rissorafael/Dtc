using Dtc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dct.Infra.Data.Context
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}