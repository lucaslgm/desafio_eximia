namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        protected Product() { }

        public Product(int id, string name, int quantity, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Nome do produto é obrigatório.");

            if (quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "A quantidade deve ser maior ou igual a zero.");

            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "O preço deve ser maior ou igual a zero.");

            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade a ser reduzida deve ser maior que zero.", nameof(quantity));

            if (Quantity < quantity)
                throw new InvalidOperationException($"Estoque insuficiente para o produto {Name}.");

            Quantity -= quantity;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade a ser aumentada deve ser maior que zero.", nameof(quantity));

            Quantity += quantity;
        }
    }
}
