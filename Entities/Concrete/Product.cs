using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string PName { get; set; }
        public string PDescription { get; set; }
        [Required]
        public int PStock { get; set; }
        [Required]
        public decimal PPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<PImage> Images { get; set; } 
        public ICollection<OrderItem>? OrderItems { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
