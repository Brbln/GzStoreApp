using AutoMapper;
using Business.Abstract;
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
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CartItemsController(
            ICartItemService cartItemService,
            ICartService cartService,
            IProductService productService,
            IMapper mapper)
        {
            _cartItemService = cartItemService;
            _cartService = cartService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartResult = _cartService.GetByUserId(userId);
            if (!cartResult.Success || cartResult.Data == null)
                return NotFound("Sepet bulunamadı");

            var itemsResult = _cartItemService.GetCartItemsDto(cartResult.Data.Id);

            if (!itemsResult.Success)
                return BadRequest(itemsResult.Message);

            return Ok(new CartDto
            {
                CartId = cartResult.Data.Id,
                Items = itemsResult.Data
            });
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddCartItemDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (dto.Quantity <= 0)
                return BadRequest("Miktar 0'dan büyük olmalıdır.");

            var cartResult = _cartService.GetByUserId(userId);
            if (!cartResult.Success || cartResult.Data == null)
                return NotFound("Sepet bulunamadı");

            var cart = cartResult.Data;

            var productResult = _productService.GetById(dto.ProductId);
            if (!productResult.Success || productResult.Data == null)
                return NotFound("Ürün bulunamadı");

            var product = productResult.Data;

            var existingItemResult =
                _cartItemService.GetByCartAndProduct(cart.Id, dto.ProductId);

            var existingItem = existingItemResult.Success ? existingItemResult.Data : null;

            var totalQuantity = dto.Quantity + (existingItem?.Quantity ?? 0);

            if (product.PStock < totalQuantity)
                return BadRequest("Yeterli stok yok");

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;

                var updateResult = _cartItemService.Update(existingItem);
                if (!updateResult.Success)
                    return BadRequest(updateResult.Message);
            }
            else
            {
                var newItem = _mapper.Map<CartItem>(dto);
                newItem.CartId = cart.Id;

                var addResult = _cartItemService.Add(newItem);
                if (!addResult.Success)
                    return BadRequest(addResult.Message);
            }

            return Ok("Ürün sepete eklendi.");
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] AddCartItemDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartResult = _cartService.GetByUserId(userId);
            if (!cartResult.Success || cartResult.Data == null)
                return NotFound("Sepet bulunamadı");

            var itemResult =
                _cartItemService.GetByCartAndProduct(cartResult.Data.Id, dto.ProductId);

            if (!itemResult.Success || itemResult.Data == null)
                return NotFound("Ürün bulunamadı");

            var item = itemResult.Data;
            item.Quantity = dto.Quantity;

            var updateResult = _cartItemService.Update(item);

            if (!updateResult.Success)
                return BadRequest(updateResult.Message);

            return Ok("Güncellendi");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartResult = _cartService.GetByUserId(userId);
            if (!cartResult.Success || cartResult.Data == null)
                return NotFound("Sepet bulunamadı");

            var itemResult = _cartItemService.GetById(id);

            if (!itemResult.Success || itemResult.Data == null)
                return NotFound("Ürün bulunamadı");

            if (itemResult.Data.CartId != cartResult.Data.Id)
                return Forbid();

            var deleteResult = _cartItemService.Delete(id);

            if (!deleteResult.Success)
                return BadRequest(deleteResult.Message);

            return Ok("Silindi");
        }
    }
}