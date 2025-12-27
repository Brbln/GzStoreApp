using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.userDto
{
    public class UserDto
    {
        [Required] 
        public string Username { get; set; }
        [Required]
        [EmailAddress] 
        public string Email { get; set; }
        [Required] 
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
    }
}
