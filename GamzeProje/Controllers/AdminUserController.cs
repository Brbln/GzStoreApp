using AutoMapper;
using Business.Abstract;
using Business.DTOs.userDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/admin/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IMapper _mapper;

        public AdminUserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var dtos = _mapper.Map<List<UserDto>>(users);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
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
                _userService.Add(dto);
                return Ok("Kullanıcı başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
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
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id); // Soft delete
                return Ok("Kullanıcı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
