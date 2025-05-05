using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public string PName { get; set; }
        public string PDescription { get; set; }
        public int PStock { get; set; }
        public decimal PPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<string>? Images { get; set; }

    }
}
