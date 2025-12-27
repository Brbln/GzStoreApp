using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.ProductDTOs
{
    public class ProductCreateDto
    {
        public string PName { get; set; }
        public string PDescription { get; set; }
        public int PStock { get; set; }
        public decimal PPrice { get; set; }
        public int CategoryId { get; set; }
        public List<PImageDto> Images { get; set; } = new();
    }
}
