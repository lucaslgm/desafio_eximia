namespace Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public int Quantity { get; private set; }
        public int OrderId { get; private set; }
        public Order Order { get; private set; }
        public Product Product { get; private set; }

        protected OrderItem() { }
        public OrderItem(int id, int productId, string productName, decimal productPrice, int quantity, int orderId)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
            OrderId = orderId;
         }

        public OrderItem(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Produto não pode ser nulo.");

            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantity));

            ProductId = product.Id;
            ProductName = product.Name;
            ProductPrice = product.Price;
            Quantity = quantity;
        }

        public decimal CalculateTotal()
        {
            return ProductPrice * Quantity;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(newQuantity));

            Quantity = newQuantity;
        }

        public void UpdateProductDetails(string productName, decimal productPrice)
        {
            ProductName = productName;
            ProductPrice = productPrice;
        }
    }
}
