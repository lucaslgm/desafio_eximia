using Core.Services;
using Domain.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailNotificationService : INotificationService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailNotificationService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task NotifyStatusChangeAsync(Order order)
        {
            var subject = $"Status do Pedido #{order.Id} Atualizado";
            var body = $"O status do seu pedido foi atualizado para: {order.Status}.";

            await SendEmailAsync(order, subject, body);
        }

        public async Task NotifyStockIssueAsync(Order order)
        {
            var subject = $"Problema de Estoque para o Pedido #{order.Id}";
            var body = "Infelizmente, há um problema no estoque de um ou mais itens do seu pedido. Estamos trabalhando para resolver isso o mais rápido possível.";

            await SendEmailAsync(order, subject, body);
        }

        private async Task SendEmailAsync(Order order, string subject, string body)
        {
            // Criar a mensagem de email
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Ecommerce", _smtpSettings.User));
            message.To.Add(new MailboxAddress("Cliente", "gomeslgm@outlook.com")); // Substitua pelo email do cliente
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                await client.AuthenticateAsync(_smtpSettings.User, _smtpSettings.Password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
