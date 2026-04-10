using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.OrderDTOs
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
