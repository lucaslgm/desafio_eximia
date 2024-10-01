using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 31, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6208), 4 },
                    { 2, new DateTime(2024, 9, 2, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6218), 4 },
                    { 3, new DateTime(2024, 9, 5, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6219), 2 },
                    { 4, new DateTime(2024, 9, 10, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6220), 0 },
                    { 5, new DateTime(2024, 9, 12, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6221), 3 },
                    { 6, new DateTime(2024, 9, 15, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6222), 4 },
                    { 7, new DateTime(2024, 9, 18, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6223), 5 },
                    { 8, new DateTime(2024, 9, 20, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6223), 6 },
                    { 9, new DateTime(2024, 9, 22, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6224), 1 },
                    { 10, new DateTime(2024, 9, 25, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6225), 2 },
                    { 11, new DateTime(2024, 9, 26, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6226), 4 },
                    { 12, new DateTime(2024, 9, 27, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6226), 0 },
                    { 13, new DateTime(2024, 9, 28, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6227), 4 },
                    { 14, new DateTime(2024, 9, 29, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6228), 3 },
                    { 15, new DateTime(2024, 9, 30, 1, 9, 7, 696, DateTimeKind.Utc).AddTicks(6228), 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Notebook", 2500.00m, 100 },
                    { 2, "Smartphone", 1500.00m, 200 },
                    { 3, "Mouse", 50.00m, 300 },
                    { 4, "Teclado", 80.00m, 150 },
                    { 5, "Monitor", 600.00m, 75 },
                    { 6, "Impressora", 800.00m, 50 },
                    { 7, "Cadeira Gamer", 1200.00m, 30 },
                    { 8, "Fone de Ouvido", 200.00m, 500 },
                    { 9, "Caixa de Som Bluetooth", 350.00m, 150 },
                    { 10, "HD Externo", 400.00m, 80 },
                    { 11, "SSD", 450.00m, 100 },
                    { 12, "Câmera de Segurança", 700.00m, 60 },
                    { 13, "Webcam", 150.00m, 120 },
                    { 14, "Mousepad", 25.00m, 300 },
                    { 15, "Notebook Cooler", 90.00m, 70 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "ProductName", "ProductPrice", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, "Notebook", 2500.00m, 1 },
                    { 2, 2, 2, "Smartphone", 1500.00m, 2 },
                    { 3, 3, 3, "Mouse", 50.00m, 5 },
                    { 4, 4, 4, "Teclado", 80.00m, 3 },
                    { 5, 5, 5, "Monitor", 600.00m, 1 },
                    { 6, 6, 6, "Impressora", 800.00m, 1 },
                    { 7, 7, 7, "Cadeira Gamer", 1200.00m, 1 },
                    { 8, 8, 8, "Fone de Ouvido", 200.00m, 4 },
                    { 9, 9, 9, "Caixa de Som Bluetooth", 350.00m, 2 },
                    { 10, 10, 10, "HD Externo", 400.00m, 1 },
                    { 11, 11, 11, "SSD", 450.00m, 1 },
                    { 12, 12, 12, "Câmera de Segurança", 700.00m, 1 },
                    { 13, 13, 13, "Webcam", 150.00m, 2 },
                    { 14, 14, 14, "Mousepad", 25.00m, 6 },
                    { 15, 15, 15, "Notebook Cooler", 90.00m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
