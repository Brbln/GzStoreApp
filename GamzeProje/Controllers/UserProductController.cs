using AutoMapper;
using Business.Abstract;
using Business.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public UserProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(_mapper.Map<List<ProductDto>>(products.Data));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(product.Data));
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            var products = _productService.GetCatById(categoryId);
            return Ok(_mapper.Map<List<ProductDto>>(products.Data));
        }

        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            var products = _productService.GetByProductName(name);
            return Ok(_mapper.Map<List<ProductDto>>(products.Data));
        }

        [HttpGet("price")]
        public IActionResult GetByPrice(decimal min, decimal max)
        {
            var products = _productService.GetByPriceRange(min, max);
            return Ok(_mapper.Map<List<ProductDto>>(products.Data));
        }
        // Admin sadece ekleme/güncelleme/silme işlemleri yapabilir
        [Route("api/admin/products")]
        [ApiController]
        [Authorize(Roles = "Admin")]
        public class AdminProductController : ControllerBase
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;

            public AdminProductController(IProductService productService, IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            [HttpPost]
            public IActionResult Add([FromBody] ProductCreateDto dto)
            {
                _productService.Add(dto);
                return Ok("Ürün başarıyla eklendi.");
            }

            [HttpPut]
            public IActionResult Update([FromBody] ProductUpdateDto dto)
            {
                _productService.Update(dto);
                return Ok("Ürün başarıyla güncellendi.");
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                _productService.Delete(id);
                return Ok("Ürün başarıyla silindi.");
            }
        }
    }
}