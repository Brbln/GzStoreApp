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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            var productsDtos = _mapper.Map<List<ProductDto>>(result);
            return Ok(productsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCatId(int categoryId)
        {
            var products = _productService.GetCatById(categoryId);
            var dtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(dtos);
        }

        [HttpGet("search")]
        public IActionResult GetByProductName([FromQuery] string name)
        {
            var products =_productService.GetByProductName(name);
            var dtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(dtos);
        }

        [HttpGet("stock/{minStock}")]
        public IActionResult GetByStock(int minStock)
        {
            var products = _productService.GetByStock(minStock);
            var dtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(dtos);
        }

        [HttpGet("price")]
        public IActionResult GetByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var products = _productService.GetByPriceRange(min, max);
            var dtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Add(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = _mapper.Map<Product>(productDto);
            _productService.Add(product);
            return Ok("Ürün başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != productDto.ProductId) return BadRequest("Id uyuşmuyor.");
            var product = _mapper.Map<Product>(productDto);
            _productService.Update(product);
            return Ok("Ürün güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        { 
            var product=_productService.GetById(id);
            if (product == null) return NotFound();
            _productService.Delete(product);
            return Ok("Ürün silindi.");
        }

        [HttpPut("update-images/{productId}")]
        public IActionResult UpdateImages(int productId, [FromBody] List<string> imageUrls)
        {
            _productService.UpdateImages(productId, imageUrls);
            return Ok("Görseller güncellendi.");
        }
    }
}
