using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string CName { get; set; }

        public ICollection<Product> Products { get; set; }
    }

}
