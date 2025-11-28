using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var cat = _categoryService.GetAll();
            var catDtos = _mapper.Map<List<CategoryDto>>(cat);
            return Ok(catDtos);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CatCreateDto createDto) {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != "1") return Forbid();   //sadece user1 ekleyebilir

            var category = _mapper.Map<Category>(createDto);
            _categoryService.Add(category);

            var catDto = _mapper.Map<CategoryDto>(category);
            return Ok(catDto);
        }

        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            var cat = _categoryService.GetById(id);
            if (cat == null) return NotFound();
            var catDtos=_mapper.Map<CategoryDto>(cat);
            return Ok(catDtos);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CatCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != "1") return Forbid();  // sadece user1 güncelleyebilir

            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();

            category.CName = updateDto.CName;
            _categoryService.Update(category);

            var catDto = _mapper.Map<CategoryDto>(category);
            return Ok(catDto);
        }
         
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != "1") return Forbid();  // sadece user1 silebilir

            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();

            _categoryService.Delete(category);
            return Ok("Kategori başarıyla silindi.");
        }
    }
}
