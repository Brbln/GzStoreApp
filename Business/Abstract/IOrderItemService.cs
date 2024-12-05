using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderItemService
    {
        public void Add(OrderItem order);
        public void Update(OrderItem order);
        public void Delete(OrderItem order);
        List<OrderItem> GetAll();
        OrderItem GetById(int id);
        List<OrderItem> GetByOrderId(int orderId);
        List<OrderItem> GetByProductId(int productId);
        OrderItem GetByOrderAndProduct(int orderId, int productId);
    }
}
