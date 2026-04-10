using AutoMapper;
using Business.Abstract;
using Business.DTOs.userDto;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("me")]
        public IActionResult GetMe()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetById(userId);
            if (!user.Success) return BadRequest(user.Message);
            var dto = _mapper.Map<UserDto>(user.Data);
            return Ok(dto);
        }

        [HttpPut("me")]
        public IActionResult UpdateMe([FromBody] UserUpdateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            dto.UserId = userId;  
            var result= _userService.UpdateUser(dto);
            return result.Success ? Ok(result.Message): BadRequest(result.Message);

        }


    }
}
