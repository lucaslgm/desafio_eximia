using Domain.Entities;
using Domain.Enums;

namespace Core.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task ProcessPaymentAsync(int orderId);
        Task SeparateOrderAsync(int orderId);
        Task CancelOrderAsync(int orderId);
        Task<OrderStatus> GetOrderStatusAsync(int orderId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
    }
}
