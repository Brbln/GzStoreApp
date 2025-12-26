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
                return NotFound("Sepet boş.");

            var cartDto = new CartDto
            {
                CartId = cartId,
                UserId = 1, 
                Items = itemsDto
            };

            return Ok(cartDto);  
        }

        [HttpPost]
        public IActionResult Add(AddCartItemDto addDto)
        { 
            var cartItem = _mapper.Map<CartItem>(addDto);
            _cartItemService.AddOrUpdate(cartItem);

            return Ok("Sepete ürün başarıyla eklendi.");
        }

        [HttpDelete("{cartItemId}")]
        public IActionResult Delete(int cartItemId)
        {
            var cartItem = _cartItemService.GetById(cartItemId);
            if (cartItem == null)
                return NotFound();

            _cartItemService.Delete(cartItem);
            return Ok("Ürün sepetten silindi.");
        }
    }
}
