using AutoMapper;
using Business.Abstract;
using Business.DTOs.OrderDTOs;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpGet("my-orders")]
        public IActionResult MyOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = _orderService.GetByUserId(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var dto = _mapper.Map<List<OrderDto>>(result.Data);

            return Ok(dto);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _orderService.GetById(id);

            if (!result.Success)
                return NotFound(result.Message);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (result.Data.UserId != userId && !User.IsInRole("Admin"))
                return Forbid();

            var dto = _mapper.Map<OrderDetailDto>(result.Data);

            return Ok(dto);
        }
        [HttpPost("{id}/cancel")]
        public IActionResult CancelOrder(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = _orderService.GetById(id);

            if (!result.Success)
                return NotFound(result.Message);

            var order = result.Data;

            if (order.UserId != userId)
                return Forbid();

            if (order.Status != OrderStatus.Pending)
                return BadRequest("Sadece bekleyen siparişler iptal edilebilir.");

            order.Status = OrderStatus.Cancelled;

            var updateResult = _orderService.Update(order);

            if (!updateResult.Success)
                return BadRequest(updateResult.Message);

            return Ok("Sipariş iptal edildi.");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();

            if (!result.Success)
                return BadRequest(result.Message);

            var dto = _mapper.Map<List<OrderDto>>(result.Data);

            return Ok(dto);
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _orderService.CreateOrderFromCart(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var result = await _orderService.UpdateStatusWithEmail(id, dto.Status, dto.TrackingNumber);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("pending-payments")]
        public IActionResult GetPendingPayments()
        {
            var result = _orderService.GetPendingPayments();
            if (!result.Success) return BadRequest(result.Message);
            var dto = _mapper.Map<List<OrderDto>>(result.Data);
            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/confirm-payment")]
        public async Task<IActionResult> ConfirmPayment(int id)
        {
            var result = await _orderService.ConfirmPayment(id);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        public class RejectPaymentDto
        {
            public string? Reason { get; set; }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/reject-payment")]
        public async Task<IActionResult> RejectPayment(int id, [FromBody] RejectPaymentDto dto)
        {
            var result = await _orderService.RejectPayment(id, dto?.Reason);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
