using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
