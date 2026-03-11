using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles ="Admin")]
        public IActionResult Add([FromBody] CatCreateDto createDto) {
              
            var category = _mapper.Map<Category>(createDto);
            _categoryService.Add(category);

            var catDto = _mapper.Map<CategoryDto>(category);
            return Ok(catDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cat = _categoryService.GetById(id);
            if (cat == null) return NotFound();
            var catDtos=_mapper.Map<CategoryDto>(cat);
            return Ok(catDtos);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles ="Admin")]
        public IActionResult Update(int id, [FromBody] CatCreateDto updateDto)
        {  
            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();

            category.CName = updateDto.CName;
            _categoryService.Update(category);

            var catDto = _mapper.Map<CategoryDto>(category);
            return Ok(catDto);
        }


        [HttpDelete("soft/{id}")]
        //[Authorize(Roles ="Admin")]
        public IActionResult SoftDelete(int id)
        {
            var result = _categoryService.SoftDelete(id);
            if (!result.Success) return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("restore/{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
            var result = _categoryService.Restore(id);
            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("hard/{id}")]
        //[Authorize(Roles ="Admin")]
        public IActionResult HardDelete(int id)
        {
            var result = _categoryService.HardDelete(id);
            if (!result.Success) return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}
