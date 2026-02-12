using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using Business.DTOs.userDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SellerController(ISellerService sellerService, IMapper mapper, IUserService userService)
        {
            _sellerService = sellerService;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetSeller()
        {
            var seller = _sellerService.GetById(1);
            if (seller == null) return NotFound();
            return Ok(_mapper.Map<SellerDto>(seller));
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
