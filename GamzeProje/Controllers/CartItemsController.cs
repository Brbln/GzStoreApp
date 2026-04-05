using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using Business.DTOs.CartDTOs;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPI.Controllers
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

        // Sepetteki ürünleri DTO ile getir
        [HttpGet("{cartId}")]
        public IActionResult GetCartItems(int cartId)
        {
            var itemsDto = _cartItemService.GetCartItemsDto(cartId);
            if (itemsDto == null || !itemsDto.Any())
                return NotFound("Sepet boş.");

            var cartDto = new CartDto
            {
                CartId = cartId,
                Items = itemsDto.ToList(),
            };

            return Ok(cartDto);
        }

        // Sepete ürün ekleme
        [HttpPost]
        public IActionResult Add([FromBody] AddCartDto addDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // DTO -> Entity map
            var cartItem = _mapper.Map<CartItem>(addDto);

            _cartItemService.AddOrUpdate(cartItem);

            return Ok("Sepete ürün başarıyla eklendi.");
        }

        // Sepetten ürün silme 
        [HttpDelete("{cartItemId}")]
        public IActionResult Delete(int cartItemId)
        {
            var result = _cartItemService.Delete(cartItemId);
            if (!result.Success)
                return NotFound(result.Message);
             
            return Ok("Ürün sepetten silindi.");
        }
    }
}
