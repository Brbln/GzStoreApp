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
  public class Order : BaseEntity, IEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime OrderTime { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; private set; }

    [Required, MaxLength(50)]
    public string Status { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public void CalculateTotalAmount()
    {
        TotalAmount = OrderItems.Sum(i => i.UnitPrice * i.Quantity);
    }
}
}
