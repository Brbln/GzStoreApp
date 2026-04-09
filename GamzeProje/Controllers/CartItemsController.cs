using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using Business.DTOs.CartDTOs;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartItemsController(ICartItemService cartItemService,ICartService cartService, IMapper mapper)
        {
            _cartItemService = cartItemService;
            _cartService = cartService;
            _mapper = mapper;
        }
         
        [HttpGet]
        public IActionResult GetCartItems()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartResult = _cartService.GetByUserId(userId);
            if (!cartResult.Success)
                return NotFound(cartResult.Message);

            var itemsResult = _cartItemService.GetCartItemsDto(cartResult.Data.Id);
            if (!itemsResult.Success)
                return BadRequest(itemsResult.Message);

            var cartDto = new CartDto
            {
                CartId = cartResult.Data.Id,
                Items = itemsResult.Data
            };

            return Ok(cartDto);
        }
         
        [HttpPost]
        public IActionResult Add([FromBody] AddCartItemDto addDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartResult = _cartService.GetByUserId(userId);
            if (!cartResult.Success)
                return NotFound(cartResult.Message);

            var cart = cartResult.Data;

            var existingItemResult =
                _cartItemService.GetByCartAndProduct(cart.Id, addDto.ProductId);

            var existingItem = existingItemResult.Success
                ? existingItemResult.Data
                : null;

            if (existingItem != null)
            {
                existingItem.Quantity += addDto.Quantity;

                var updateResult = _cartItemService.Update(existingItem);
                if (!updateResult.Success)
                    return BadRequest(updateResult.Message);
            }
            else
            {
                var cartItem = _mapper.Map<CartItem>(addDto);
                cartItem.CartId = cart.Id;

                var addResult = _cartItemService.Add(cartItem);
                if (!addResult.Success)
                    return BadRequest(addResult.Message);
            }

            return Ok("Ürün sepete eklendi.");
        }
        [HttpPut("update")]
        public IActionResult UpdateCartItem([FromBody] AddCartDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartResult = _cartService.GetByUserId(userId);
            if (!cartResult.Success)
                return NotFound(cartResult.Message);

            var itemResult =
                _cartItemService.GetByCartAndProduct(cartResult.Data.Id, dto.ProductId);

            if (!itemResult.Success || itemResult.Data == null)
                return NotFound("Sepette ürün bulunamadı.");

            var item = itemResult.Data;
            item.Quantity = dto.Quantity;

            var updateResult = _cartItemService.Update(item);

            if (!updateResult.Success)
                return BadRequest(updateResult.Message);

            return Ok(updateResult.Message);
        }


        [HttpDelete("{cartItemId}")]
        public IActionResult Delete(int cartItemId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartResult = _cartService.GetByUserId(userId);
            if (!cartResult.Success)
                return NotFound(cartResult.Message);

            var cart = cartResult.Data;

            var itemResult = _cartItemService.GetById(cartItemId);
            if (!itemResult.Success || itemResult.Data == null)
                return NotFound(itemResult.Message);

            var cartItem = itemResult.Data;

            if (cartItem.CartId != cart.Id)
                return Forbid("Bu ürünü silme yetkiniz yok.");

            var deleteResult = _cartItemService.Delete(cartItemId);

            if (!deleteResult.Success)
                return BadRequest(deleteResult.Message);

            return Ok(deleteResult.Message);
        }
    }
}
