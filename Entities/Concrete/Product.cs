using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : BaseEntity, IEntity
    { 
        [Required, MaxLength(100)]
        public string PName { get; set; }

        [MaxLength(1000)]
        public string? PDescription { get; set; }

        [Required]
        public int PStock { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal PPrice { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<PImage> Images { get; set; } = new();

        public ICollection<OrderItem>? OrderItems { get; set; } 
    }
}
