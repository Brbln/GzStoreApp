using AutoMapper;
using Business.Abstract;
using Business.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/seller/products")]
    [ApiController]
    // [Authorize(Roles = "Seller")]
    public class SellerProductController : ControllerBase

    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public SellerProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllForSeller();
            var dtos = _mapper.Map<List<ProductDto>>(products.Data);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetByIdForSeller(id);
            if (product == null)
                return NotFound();

            var dto = _mapper.Map<ProductDto>(product.Data);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Add(ProductCreateDto dto)
        {
            _productService.Add(dto);
            return Ok("Ürün eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductUpdateDto dto)
        {
            if (id != dto.ProductId)
                return BadRequest("Id uyuşmuyor.");

            _productService.Update(dto);
            return Ok("Ürün güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult SoftDelete(int id)
        {
            _productService.Delete(id);
            return Ok("Ürün satıştan kaldırıldı.");
        }

        [HttpPut("{id}/restore")]
        public IActionResult Restore(int id)
        {
            _productService.Restore(id);
            return Ok("Ürün tekrar satışa açıldı.");
        }

        [HttpDelete("{id}/hard")]
        public IActionResult HardDelete(int id)
        {
            _productService.HardDelete(id);
            return Ok("Ürün tamamen silindi.");
        }
    }
}
