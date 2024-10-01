using Core.Services;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class PaymentProcessor : IPaymentProcessor
    {
        public async Task<PaymentResult> ProcessAsync(Order order)
        {
            // Simulação de processamento de pagamento
            PaymentResult result = new();

            // Simulando verificação de método de pagamento (por exemplo, baseado em alguma propriedade do pedido)
            if (order.CalculateTotal() > 0)
            {
                // Lógica fictícia: se o valor total for maior que zero, o pagamento será aprovado
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Valor do pedido inválido.";
            }

            // Simulando o tempo de processamento do pagamento
            await Task.Delay(1000); // Aguarda 1 segundo para simular processamento

            return result;
        }
    }
}
