using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PImage : IEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required, MaxLength(300)]
        public string ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Product? Product { get; set; }
    }

}
