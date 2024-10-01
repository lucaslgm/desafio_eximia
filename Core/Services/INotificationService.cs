using Domain.Entities;

namespace Core.Services
{
    public interface INotificationService
    {
        Task NotifyStatusChangeAsync(Order order);
        Task NotifyStockIssueAsync(Order order);
    }
}
