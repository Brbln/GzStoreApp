using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Seller : BaseEntity, IEntity
    {
        [Required, MaxLength(100)]
        public string SellerName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string? PhoneNo { get; set; }

        public ICollection<Product> Products { get; set; }
    }

}
