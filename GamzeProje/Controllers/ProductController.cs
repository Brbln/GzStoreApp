using AutoMapper;
using Business.Abstract;
using Business.DTOs.ProductDTOs;
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
            if (!result.Success)
                return BadRequest(result.Message);

            var dtos = _mapper.Map<List<ProductDto>>(result.Data);
            return Ok(dtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);

            if (!result.Success)
                return NotFound(result.Message);

            var dto = _mapper.Map<ProductDto>(result.Data);
            return Ok(dto);
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCatId(int categoryId)
        {
            var result = _productService.GetCatById(categoryId);
            var dtos = _mapper.Map<List<ProductDto>>(result.Data);
            return Ok(dtos);
        }
        [HttpGet("search")]
        public IActionResult GetByProductName([FromQuery] string name)
        {
            var result = _productService.GetByProductName(name);
            var dtos = _mapper.Map<List<ProductDto>>(result.Data);
            return Ok(dtos);
        }

        [HttpGet("stock/{minStock}")]
        public IActionResult GetByStock(int minStock)
        {
            var result = _productService.GetByStock(minStock);
            var dtos = _mapper.Map<List<ProductDto>>(result.Data);
            return Ok(dtos);
        }

        [HttpGet("price")]
        public IActionResult GetByPriceRange(
            [FromQuery] decimal min,
            [FromQuery] decimal max)
        {
            var result = _productService.GetByPriceRange(min, max);
            var dtos = _mapper.Map<List<ProductDto>>(result.Data);
            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Add(ProductCreateDto dto)
        {
            var result = _productService.Add(dto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [HttpPut]
        public IActionResult Update(ProductUpdateDto dto)
        {
            var result = _productService.Update(dto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("soft/{id}")]
        public IActionResult SoftDelete(int id)
        {
            var result = _productService.Delete(id);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
        [HttpPut("restore/{id}")]
        public IActionResult Restore(int id)
        {
            var result = _productService.Restore(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("hard/{id}")]
        public IActionResult HardDelete(int id)
        {
            var result = _productService.HardDelete(id);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
        //[HttpPut("update-images/{productId}")]
        //public IActionResult UpdateImages(
        //  int productId,
        //  [FromBody] List<string> imageUrls)
        //{
        //    var result = _productService.UpdateImages(productId, imageUrls);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}
    }
}
