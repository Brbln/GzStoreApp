using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCartItems(int cartId)
        {
            var itemsDto = _cartItemService.GetCartItemsDto(cartId);
            if (itemsDto == null || !itemsDto.Any())
                return NotFound();

            return Ok(itemsDto);
        }
    }
}
