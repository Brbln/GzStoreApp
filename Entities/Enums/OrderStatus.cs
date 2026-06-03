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
        Processing,        // Hazırlanıyor
        Shipped,     // Kargoya verildi
        Delivered,   // Teslim edildi
        Cancelled    // İptal edildi
    }
}
