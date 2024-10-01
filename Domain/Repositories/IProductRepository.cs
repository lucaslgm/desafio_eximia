using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int productId);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(Product product);
        Task AddAsync(Product product);
        Task DeleteAsync(int id);
    }
}
