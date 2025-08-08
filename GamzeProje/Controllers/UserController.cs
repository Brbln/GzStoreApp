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
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpPost]
        public IActionResult Add([FromBody]UserCreateDto ucdto ) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = _mapper.Map<User>(ucdto);                 
                user.Password = ucdto.Password;

                _userService.Add(user);
                return Ok("Kullanıcı başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody]UserCreateDto ucdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = _userService.GetByUserName(ucdto.UserName);
            if (existingUser == null)
                return NotFound("Kullanıcı bulunamadı.");

            try
            {
                existingUser.Password = ucdto.Password;
                existingUser.Address = ucdto.Address;
                existingUser.PhoneNo = ucdto.PhoneNo;

                _userService.Update(existingUser);
                return Ok("Kullanıcı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            var userdto= _mapper.Map<UserDto>(user);
            return Ok(userdto);
        }

        [HttpGet("email")]
        public IActionResult GetByEmail([FromQuery] string email)
        {
            var user = _userService.GetByEmail(email);
            if (user == null) return NotFound();
            var userdto = _mapper.Map<UserDto>(user);
            return Ok(userdto);
        }

        [HttpGet("username")]
        public IActionResult GetByUserName([FromQuery] string username)
        {
            var user = _userService.GetByUserName(username);
            if (user == null) return NotFound();
            var userdto = _mapper.Map<UserDto>(user);
            return Ok(userdto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            _userService.Delete(user);
            return Ok("Kullanıcı başarıyla silindi.");
        }

    }
}
