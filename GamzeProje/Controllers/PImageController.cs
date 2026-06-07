using Business.Abstract;
using Business.DTOs.ImageDTOs;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PImageController : ControllerBase
    {
        private readonly IPImageService _imageService;
        public PImageController(IPImageService imageService)
        {
            _imageService = imageService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public IActionResult Add([FromBody] AddImgDto dto)
        {
            var result = _imageService.Add(dto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdImgDto dto)
        {
            var result = _imageService.Update(dto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _imageService.Delete(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
         
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _imageService.GetById(id);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetByProductId(int productId)
        {
            var result = _imageService.GetByProductId(productId);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _imageService.GetAll();

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
