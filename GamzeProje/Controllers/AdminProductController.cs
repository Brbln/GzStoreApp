using AutoMapper;
using Business.Abstract;
using Business.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")] 
    public class AdminProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;
         
        public AdminProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("products")]
        public IActionResult GetAllProduct()
        {
            var products = _productService.GetAllForAdmin();
            var dtos = _mapper.Map<List<ProductDto>>(products.Data);
            return Ok(dtos);

        }

        [HttpGet("{id}")]
        public IActionResult GetByProdId(int id)
        {
            var product = _productService.GetByIdForAdmin(id);
            if (product == null)
                return NotFound();

            var dto = _mapper.Map<ProductDto>(product.Data);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductCreateDto dto)
        {
            var result = _productService.Add(dto);
            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductUpdateDto dto)
        {
            if (id != dto.ProductId) return BadRequest("Id uyuşmuyor.");

            var result = _productService.Update(dto);
            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("soft/{id}")]
        public IActionResult SoftDelete(int id)
        {
            var result = _productService.Delete(id);
            if (!result.Success) return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("restore/{id}")]
        public IActionResult Restore(int id)
        {
            var result = _productService.Restore(id);
            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("hard/{id}")]
        public IActionResult HardDelete(int id)
        {
            var result = _productService.HardDelete(id);
            if (!result.Success) return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}
