using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PImage:IEntity
    {
        public int Id { get; set; }   
        public int ProductId { get; set; }      
        public string ImageUrl { get; set; }     
        public DateTime CreatedDate { get; set; }     
        
        public Product Product { get; set; }
    }
}
