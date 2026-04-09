using Business.Abstract;
using Business.DTOs.CartDTOs;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ICartItemService _cartItemService;

    public CartController(ICartService cartService, ICartItemService cartItemService)
    {
        _cartService = cartService;
        _cartItemService = cartItemService;
    }

    [HttpGet("my-cart")]
    public ActionResult<CartDto> GetMyCart()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var cartResult = _cartService.GetByUserId(userId);

        if (!cartResult.Success)
            return NotFound(cartResult.Message);

        var items = _cartItemService.GetCartItemsDto(cartResult.Data.Id);

        return Ok(new CartDto
        {
            CartId = cartResult.Data.Id,
            Items = items.Data
        });
    }

    [HttpPost("add")]
    public IActionResult AddToCart([FromBody] AddCartDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (dto.Quantity <= 0)
            return BadRequest("Miktar 0'dan büyük olmalıdır.");

        var cartResult = _cartService.GetByUserId(userId);

        if (!cartResult.Success || cartResult.Data == null)
            return NotFound(cartResult.Message);

        var cart = cartResult.Data;

        var itemResult =
            _cartItemService.GetByCartAndProduct(cart.Id, dto.ProductId);

        var existingItem = itemResult.Success ? itemResult.Data : null;

        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;

            var updateResult = _cartItemService.Update(existingItem);

            if (!updateResult.Success)
                return BadRequest(updateResult.Message);
        }
        else
        {
            var newItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            var addResult = _cartItemService.Add(newItem);

            if (!addResult.Success)
                return BadRequest(addResult.Message);
        }

        return Ok("Ürün sepete eklendi.");
    }

    [HttpDelete("clear")]
    public IActionResult ClearCart()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var cartResult = _cartService.GetByUserId(userId);

        if (!cartResult.Success || cartResult.Data == null)
            return NotFound(cartResult.Message);

        var result = _cartItemService.ClearCart(cartResult.Data.Id);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok("Sepet temizlendi.");
    }
}