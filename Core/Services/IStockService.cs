using Domain.Entities;

namespace Core.Services
{
    public interface IStockService
    {
        Task<StockResult> CheckAndReduceStockAsync(IEnumerable<OrderItem> items);
    }

    public class StockResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
