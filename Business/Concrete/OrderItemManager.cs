using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderItemManager : IOrderItemService
    {
        IOrderItemDal _oItemDal;

        public OrderItemManager(IOrderItemDal oItemDal)
        {
            _oItemDal = oItemDal;
        }

        public void Add(OrderItem order)
        {
            _oItemDal.Add(order);
        }

        public void Delete(OrderItem order)
        {
            _oItemDal.Delete(order);
        }

        public List<OrderItem> GetAll()
        {
            return _oItemDal.GetAll();
        }

        public OrderItem GetById(int id)
        {
            return _oItemDal.Get(a => a.OrderItemId == id);
        }

        public OrderItem GetByOrderAndProduct(int orderId, int productId)
        {
            return _oItemDal.Get(a => a.ProductId==productId && a.OrderId==orderId);
        }

        public List<OrderItem> GetByOrderId(int orderId)
        {
            return _oItemDal.GetAll(a => a.OrderId == orderId);
        }

        public List<OrderItem> GetByProductId(int productId)
        {
            return _oItemDal.GetAll(a=>a.ProductId==productId);
        }

        public void Update(OrderItem order)
        {
            _oItemDal.Update(order);
        }
    }
}
