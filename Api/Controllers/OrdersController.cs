using Application.DTOs;
using AutoMapper;
using Core.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Mapeando o DTO para a entidade
            var order = _mapper.Map<Order>(orderDto);
            await _orderService.CreateOrderAsync(order);

            // Retornando o pedido criado, mapeado de volta para o DTO
            var createdOrderDto = _mapper.Map<OrderDto>(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrderDto.Id }, createdOrderDto);
        }

        [HttpPost("{orderId}/process-payment")]
        public async Task<IActionResult> ProcessPayment(int orderId)
        {
            await _orderService.ProcessPaymentAsync(orderId);
            return NoContent(); // Preferível retornar NoContent para operações bem-sucedidas sem resposta
        }

        [HttpPost("{orderId}/separate-order")]
        public async Task<IActionResult> SeparateOrder(int orderId)
        {
            await _orderService.SeparateOrderAsync(orderId);
            return NoContent();
        }

        [HttpPost("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            await _orderService.CancelOrderAsync(orderId);
            return NoContent();
        }

        [HttpGet("{orderId}/status")]
        public async Task<IActionResult> GetOrderStatus(int orderId)
        {
            var status = await _orderService.GetOrderStatusAsync(orderId);

            return Ok(status);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(ordersDto);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(OrderStatus status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(ordersDto);
        }
    }
}
