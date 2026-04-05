using Business.Abstract;
using Business.DTOs.CartDTOs;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize] // Sadece giriş yapmış kullanıcılar
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ICartItemService _cartItemService;

    public CartController(ICartService cartService, ICartItemService cartItemService)
    {
        _cartService = cartService;
        _cartItemService = cartItemService;
    }

    // GET: api/cart/my-cart
    [HttpGet("my-cart")]
    public ActionResult<CartDto> GetMyCart()
    {
        var userId = int.Parse(User.FindFirst("Id").Value); 
        var cart = _cartService.GetByUserId(userId);

        if (cart == null)
            return NotFound("Sepet bulunamadı.");

        var itemsDto = _cartItemService.GetCartItemsDto(cart.Id);

        var cartDto = new CartDto
        {
            CartId = cart.Id,
            Items = itemsDto
        };

        return Ok(cartDto);
    }

    // POST: api/cart/add
    [HttpPost("add")]
    public IActionResult AddToCart([FromBody] AddCartDto dto)
    {
        var userId = int.Parse(User.FindFirst("Id").Value);
        var cart = _cartService.GetByUserId(userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _cartService.Add(cart);
        }

        var cartItem = new CartItem
        {
            CartId = cart.Id,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };

        _cartItemService.AddOrUpdate(cartItem);

        return Ok("Ürün sepete eklendi.");
    }

    // PUT: api/cart/update
    [HttpPut("update")]
    public IActionResult UpdateCartItem([FromBody] AddCartDto dto)
    {
        var userId = int.Parse(User.FindFirst("Id").Value);
        var cart = _cartService.GetByUserId(userId);

        if (cart == null)
            return NotFound("Sepet bulunamadı.");

        var cartItem = new CartItem
        {
            CartId = cart.Id,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };

        _cartItemService.Update(cartItem);

        return Ok("Sepet güncellendi.");
    }
}