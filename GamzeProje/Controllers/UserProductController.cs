using AutoMapper;
using Business.Abstract;
using Business.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductController :ControllerBase
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
    }
}
