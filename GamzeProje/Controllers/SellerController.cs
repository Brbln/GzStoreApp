using AutoMapper;
using Business.Abstract;
using Business.DTOs.userDto;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    { 
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SellerController(  IMapper mapper, IUserService userService)
        { 
            _userService = userService;
            _mapper = mapper;
        } 

        [HttpGet("users/deleted")]
        public IActionResult GetDeletedUsers()
        {
            var users = _userService.GetDeletedUsers();
            var dtos = _mapper.Map<List<UserDto>>(users);
            return Ok(dtos);
        }
    }
}
