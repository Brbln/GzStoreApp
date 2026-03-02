using Business.Abstract;
using Business.DTOs;
using Entities.Concrete; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISellerService _sellerService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, ISellerService sellerService, IConfiguration configuration)
        {
            _userService = userService;
            _sellerService = sellerService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _userService.ValidateUser(dto.Email, dto.Password);
            var seller = _sellerService.ValidateSeller(dto.Email, dto.Password);

            object loginEntity;
            string role;

            if (seller != null)
            {
                loginEntity = seller;
                role = "Seller";
            }
            else if (user != null)
            {
                loginEntity = user;
                role = "User";
            }
            else
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(loginEntity, role);
            return Ok(new { token });
        }

        private string GenerateJwtToken(object entity, string role)
        {
            string email = entity switch
            {
                User u => u.Email,
                Seller s => s.Email,
                _ => ""
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
