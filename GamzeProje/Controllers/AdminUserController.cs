using AutoMapper;
using Business.Abstract;
using Business.DTOs.userDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("users")] 
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            if (!users.Success)
                return BadRequest(users.Message);
            var dtos = _mapper.Map<List<UserDto>>(users.Data);
            return Ok(dtos);
        }

        [HttpGet("users/{id}")] 
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetById(id);
            if (!user.Success)
                return NotFound(user.Message);
            var dto = _mapper.Map<UserDto>(user.Data);
            return Ok(dto);
        }

        [HttpGet("email")]
        public IActionResult GetUserByEmail([FromQuery] string email)
        {
            var user = _userService.GetByEmail(email);
            if (!user.Success)
                return NotFound(user.Message);
            var dto = _mapper.Map<UserDto>(user.Data);
            return Ok(dto);
        }

        [HttpGet("username")]
        public IActionResult GetByUserName([FromQuery] string username)
        {
            var user = _userService.GetByUserName(username);
            if (!user.Success) return NotFound(user.Message);
            var dto = _mapper.Map<UserDto>(user.Data);
            return Ok(dto);
        }

        
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.UpdateUser(dto);
            if (!result.Success) return Conflict(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult UserDelete(int id)
        {
            var result = _userService.Delete(id);
            if (!result.Success) return NotFound(result.Message);

            return Ok(result.Message);
        }
        [HttpGet("users/deleted")]
        public IActionResult GetDeletedUsers()
        {
            var users = _userService.GetDeletedUsers();
            if (!users.Success)
                return BadRequest(users.Message);
            var dtos = _mapper.Map<List<UserDto>>(users.Data);
            return Ok(dtos);
        }
    }
}
