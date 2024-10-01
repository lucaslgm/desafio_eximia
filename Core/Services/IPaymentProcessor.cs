using Domain.Entities;

namespace Core.Services
{
    public interface IPaymentProcessor
    {
        Task<PaymentResult> ProcessAsync(Order order);
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
