using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _mapper;

        public CartItemsController(ICartItemService cartItemService, IMapper mapper)
        {
            _cartItemService = cartItemService;
            _mapper = mapper;
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCartItems(int cartId)
        {
            var itemsDto = _cartItemService.GetCartItemsDto(cartId);
            if (itemsDto == null || !itemsDto.Any())
                return NotFound();

            return Ok(itemsDto);
        }
        [HttpPost]
        public IActionResult Add(CartItemDto cartItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartItem = _mapper.Map<CartItem>(cartItemDto);
            _cartItemService.Add(cartItem);
            return Ok("Sepete ürün başarıyla eklendi.");
        }
    }
}
