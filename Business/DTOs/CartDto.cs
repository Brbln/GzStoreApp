using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Business.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> Items { get; set; }
        public decimal TotalAmount => Items?.Sum(i => i.Quantity * i.UnitPrice) ?? 0;
    }
}
