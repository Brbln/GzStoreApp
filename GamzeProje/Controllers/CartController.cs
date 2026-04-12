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
    public IActionResult GetMyCart()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var cartResult = _cartService.GetByUserId(userId);
        if (!cartResult.Success || cartResult.Data == null)
            return NotFound("Sepet bulunamadı");

        var items = _cartItemService.GetCartItemsDto(cartResult.Data.Id);

        return Ok(new CartDto
        {
            CartId = cartResult.Data.Id,
            Items = items.Data
        });
    }

    [HttpDelete("clear")]
    public IActionResult ClearCart()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var cartResult = _cartService.GetByUserId(userId);
        if (!cartResult.Success || cartResult.Data == null)
            return NotFound("Sepet bulunamadı");

        var result = _cartItemService.ClearCart(cartResult.Data.Id);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok("Sepet temizlendi.");
    }
}