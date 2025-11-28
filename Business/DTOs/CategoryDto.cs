using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; } 

        public string CName { get; set; }

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
