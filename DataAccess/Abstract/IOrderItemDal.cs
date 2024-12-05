using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderItemDal:IEntityRepository<OrderItem>
    {

        // Belirli bir sipariş ID'sine ait tüm ürünleri getirir
        List<OrderItem> GetByOrderId(int orderId);

        // Belirli bir ürün ID'sine ait tüm siparişleri getirir
        List<OrderItem> GetByProductId(int productId);

        // Belirli bir sipariş ve ürün kombinasyonuna ait sipariş öğesini getirir
        OrderItem GetByOrderAndProduct(int orderId, int productId);
    }
}
