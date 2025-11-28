using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        public string PName { get; set; }
        public string PDescription { get; set; } 

        [Range(0, int.MaxValue)]
        public int PStock { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public List<PImageDto> Images { get; set; } = new List<PImageDto>();
    }
}
