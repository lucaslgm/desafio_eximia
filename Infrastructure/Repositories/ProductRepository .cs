using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _dbContext;

        public ProductRepository(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product?> GetByIdAsync(int productId)
        {
            return await _dbContext.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            // Atualiza o produto no contexto
            _dbContext.Products.Update(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
