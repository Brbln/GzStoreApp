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
        public IActionResult Add([FromBody] CatCreateDto catDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cat = _mapper.Map<Category>(catDto);
            _categoryService.Add(cat);
            return Ok("Kategori başarıyla eklendi.");
        }


        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            var cat = _categoryService.GetById(id);
            if (cat == null) return NotFound();
            var catDtos=_mapper.Map<CategoryDto>(cat);
            return Ok(catDtos);
        }
    }
}
