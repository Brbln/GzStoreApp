using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum OrderStatus
    {
        Pending,     // Sipariş alındı
        Paid,        // Ödeme yapıldı
        Shipped,     // Kargoya verildi
        Completed,   // Tamamlandı
        Cancelled    // İptal edildi
    }
}
