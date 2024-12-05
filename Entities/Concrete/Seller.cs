using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Seller : IEntity
    {
        public int SellerId { get; set; } = 1;
        public string SellerName { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; }
        public string PhoneNo { get; set; }

    }
}
