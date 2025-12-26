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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var dtos = _mapper.Map<List<UserDto>>(users);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");
            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        [HttpGet("email")]
        public IActionResult GetByEmail([FromQuery] string email)
        {
            var user = _userService.GetByEmail(email);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");
            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        [HttpGet("username")]
        public IActionResult GetByUserName([FromQuery] string username)
        {
            var user = _userService.GetByUserName(username);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");
            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = _mapper.Map<User>(dto);
                _userService.Add(user);
                return Ok("Kullanıcı başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message); // DB çakışması için 409
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _userService.UpdateUser(dto);
                return Ok("Kullanıcı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message); // DB çakışması için 409
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");
            _userService.Delete(user);
            return Ok("Kullanıcı başarıyla silindi.");
        }

    }
}
