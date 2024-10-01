using Core.Services;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;

        public StockService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<StockResult> CheckAndReduceStockAsync(IEnumerable<OrderItem> items)
        {
            var result = new StockResult();

            foreach (var item in items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                {
                    result.Success = false;
                    result.ErrorMessage = $"Produto com ID {item.ProductId} não foi encontrado no estoque.";
                    return result;
                }

                try
                {
                    product.ReduceStock(item.Quantity);

                    // Atualizar as informações do pedido com os detalhes do produto no momento da compra
                    item.UpdateProductDetails(product.Name, product.Price);
                }
                catch (InvalidOperationException ex)
                {
                    result.Success = false;
                    result.ErrorMessage = ex.Message;
                    return result;
                }
            }

            // Atualizar todos os produtos no banco de dados
            foreach (var item in items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    await _productRepository.UpdateAsync(product);
                }
            }

            result.Success = true;
            return result;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<int> GetProductQuantityAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product?.Quantity ?? 0;
        }
    }
}
