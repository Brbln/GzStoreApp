using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class SellerDto
    {
        public int SellerId { get; set; } = 1;
        public string SellerName { get; set; } 
        public string Email { get; set; }
        public string PhoneNo { get; set; }
    }
}
