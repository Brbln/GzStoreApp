using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.ProductDTOs
{
    public class ProductUpdateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string PName { get; set; }

        public string? PDescription { get; set; }

        [Range(0, int.MaxValue)]
        public int PStock { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal PPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
