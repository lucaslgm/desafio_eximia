using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
        Task UpdateAsync(Order order);
        Task<IEnumerable<Order>> GetAllAsync();
    }
}
