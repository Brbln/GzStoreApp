using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public string ProductName { get; set; }  // Product entity’den gelir
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
