using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }
        public ICollection<OrderItem> Items { get; private set; } = [];

        // Construtor padrão
        public Order()
        {
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.AguardandoProcessamento;
        }

        public Order(int id, DateTime createdAt, OrderStatus status)
        {
            Id = id;
            CreatedAt = createdAt;
            Status = status;
        }

        public void AddItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item do pedido não pode ser nulo.");

            Items.Add(item);
        }

        public decimal CalculateTotal()
        {
            return Items.Sum(item => item.CalculateTotal());
        }

        public void SetStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
