using Core.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IStockService _stockService;
        private readonly INotificationService _notificationService;

        public OrderService(
            IOrderRepository orderRepository,
            IPaymentProcessor paymentProcessor,
            IStockService stockService,
            INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _paymentProcessor = paymentProcessor;
            _stockService = stockService;
            _notificationService = notificationService;
        }

        public async Task CreateOrderAsync(Order order)
        {
            order.SetStatus(OrderStatus.AguardandoProcessamento);

            await _orderRepository.AddAsync(order);

            await NotifyStatusChangeAsync(order);
        }

        public async Task ProcessPaymentAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);

            ValidateOrderStatus(order, OrderStatus.AguardandoProcessamento);

            order.SetStatus(OrderStatus.ProcessandoPagamento);

            await NotifyStatusChangeAsync(order);

            var paymentResult = await RetryAsync(() => _paymentProcessor.ProcessAsync(order), 3);

            if (paymentResult.Success)
            {
                order.SetStatus(OrderStatus.PagamentoConcluido);
            }
            else
            {
                order.SetStatus(OrderStatus.Cancelado);
            }

            await _orderRepository.UpdateAsync(order);

            await NotifyStatusChangeAsync(order);
        }

        public async Task SeparateOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);

            ValidateOrderStatus(order, OrderStatus.PagamentoConcluido);

            order.SetStatus(OrderStatus.SeparandoPedido);

            await NotifyStatusChangeAsync(order);

            var stockResult = await _stockService.CheckAndReduceStockAsync(order.Items);

            order.SetStatus(stockResult.Success ? OrderStatus.Concluido : OrderStatus.AguardandoEstoque);

            await _orderRepository.UpdateAsync(order);

            if (!stockResult.Success)
            {
                await _notificationService.NotifyStockIssueAsync(order);
            }

            await NotifyStatusChangeAsync(order);
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);

            if (order.Status == OrderStatus.Concluido)
            {
                throw new InvalidOperationException("Pedidos concluídos não podem ser cancelados.");
            }

            order.SetStatus(OrderStatus.Cancelado);
            await _orderRepository.UpdateAsync(order);
            await NotifyStatusChangeAsync(order);
        }

        public async Task<OrderStatus> GetOrderStatusAsync(int orderId) =>
            (await GetOrderByIdAsync(orderId)).Status;

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() => await _orderRepository.GetAllAsync();

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var allOrders = await _orderRepository.GetAllAsync();

            return allOrders.Where(order => order.Status == status);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId) =>
            await _orderRepository.GetByIdAsync(orderId) ?? throw new KeyNotFoundException($"Pedido com ID {orderId} não foi encontrado.");

        private static void ValidateOrderStatus(Order order, OrderStatus expectedStatus)
        {
            if (order.Status != expectedStatus)
                throw new InvalidOperationException($"O status do pedido não é '{expectedStatus}'.");
        }

        private async Task NotifyStatusChangeAsync(Order order) =>
            await _notificationService.NotifyStatusChangeAsync(order);

        private static async Task<T> RetryAsync<T>(Func<Task<T>> action, int maxRetries) where T : class
        {
            int attempts = 0;
            T result;
            do
            {
                result = await action();
                attempts++;
            } while (result == null && attempts < maxRetries);

            if (result == null)
            {
                throw new InvalidOperationException("Result cannot be null.");
            }

            return result;
        }
    }
}
