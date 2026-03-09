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
    [Authorize(Roles = "User,Admin")]
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
        [Authorize(Roles = "User,Admin")]  
        public IActionResult GetMe()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetById(userId);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");
            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        [HttpPut("me")]
        [Authorize(Roles = "User,Admin")]  
        public IActionResult UpdateMe([FromBody] UserUpdateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            dto.UserId = userId;  

            try
            {
                _userService.UpdateUser(dto);
                return Ok("Kullanıcı bilgileri başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }



    }
}
