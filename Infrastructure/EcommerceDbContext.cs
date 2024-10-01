using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(o => o.CreatedAt).IsRequired();
                entity.Property(o => o.Status).IsRequired();

                entity.HasMany(o => o.Items)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração da entidade OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);

                entity.Property(oi => oi.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(oi => oi.Quantity).IsRequired();
                entity.Property(oi => oi.ProductName).IsRequired().HasMaxLength(100);
                entity.Property(oi => oi.ProductPrice).HasPrecision(12, 2).IsRequired();

                entity.HasOne(oi => oi.Product)
                      .WithMany()
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração da entidade Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Quantity).IsRequired();
                entity.Property(p => p.Price).HasPrecision(12, 2).IsRequired();
            });

            // Seeding da entidade Product
            modelBuilder.Entity<Product>().HasData(
                new Product(1, "Notebook", 100, 2500.00m),
                new Product(2, "Smartphone", 200, 1500.00m),
                new Product(3, "Mouse", 300, 50.00m),
                new Product(4, "Teclado", 150, 80.00m),
                new Product(5, "Monitor", 75, 600.00m),
                new Product(6, "Impressora", 50, 800.00m),
                new Product(7, "Cadeira Gamer", 30, 1200.00m),
                new Product(8, "Fone de Ouvido", 500, 200.00m),
                new Product(9, "Caixa de Som Bluetooth", 150, 350.00m),
                new Product(10, "HD Externo", 80, 400.00m),
                new Product(11, "SSD", 100, 450.00m),
                new Product(12, "Câmera de Segurança", 60, 700.00m),
                new Product(13, "Webcam", 120, 150.00m),
                new Product(14, "Mousepad", 300, 25.00m),
                new Product(15, "Notebook Cooler", 70, 90.00m)
            );

            // Seeding da entidade Order
            modelBuilder.Entity<Order>().HasData(
                new Order(1, DateTime.UtcNow.AddDays(-30), OrderStatus.Concluido),
                new Order(2, DateTime.UtcNow.AddDays(-28), OrderStatus.Concluido),
                new Order(3, DateTime.UtcNow.AddDays(-25), OrderStatus.PagamentoConcluido),
                new Order(4, DateTime.UtcNow.AddDays(-20), OrderStatus.AguardandoProcessamento),
                new Order(5, DateTime.UtcNow.AddDays(-18), OrderStatus.SeparandoPedido),
                new Order(6, DateTime.UtcNow.AddDays(-15), OrderStatus.Concluido),
                new Order(7, DateTime.UtcNow.AddDays(-12), OrderStatus.AguardandoEstoque),
                new Order(8, DateTime.UtcNow.AddDays(-10), OrderStatus.Cancelado),
                new Order(9, DateTime.UtcNow.AddDays(-8), OrderStatus.ProcessandoPagamento),
                new Order(10, DateTime.UtcNow.AddDays(-5), OrderStatus.PagamentoConcluido),
                new Order(11, DateTime.UtcNow.AddDays(-4), OrderStatus.Concluido),
                new Order(12, DateTime.UtcNow.AddDays(-3), OrderStatus.AguardandoProcessamento),
                new Order(13, DateTime.UtcNow.AddDays(-2), OrderStatus.Concluido),
                new Order(14, DateTime.UtcNow.AddDays(-1), OrderStatus.SeparandoPedido),
                new Order(15, DateTime.UtcNow, OrderStatus.PagamentoConcluido)
            );

            // Seeding da entidade OrderItem
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem(1, 1, "Notebook", 2500.00m, 1, 1),
                new OrderItem(2, 2, "Smartphone", 1500.00m, 2, 2),
                new OrderItem(3, 3, "Mouse", 50.00m, 5, 3),
                new OrderItem(4, 4, "Teclado", 80.00m, 3, 4),
                new OrderItem(5, 5, "Monitor", 600.00m, 1, 5),
                new OrderItem(6, 6, "Impressora", 800.00m, 1, 6),
                new OrderItem(7, 7, "Cadeira Gamer", 1200.00m, 1, 7),
                new OrderItem(8, 8, "Fone de Ouvido", 200.00m, 4, 8),
                new OrderItem(9, 9, "Caixa de Som Bluetooth", 350.00m, 2, 9),
                new OrderItem(10, 10, "HD Externo", 400.00m, 1, 10),
                new OrderItem(11, 11, "SSD", 450.00m, 1, 11),
                new OrderItem(12, 12, "Câmera de Segurança", 700.00m, 1, 12),
                new OrderItem(13, 13, "Webcam", 150.00m, 2, 13),
                new OrderItem(14, 14, "Mousepad", 25.00m, 6, 14),
                new OrderItem(15, 15, "Notebook Cooler", 90.00m, 1, 15)
            );
        }
    }
}
