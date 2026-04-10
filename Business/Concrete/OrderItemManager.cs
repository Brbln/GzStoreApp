using Business.Abstract;
using Core.Utilities.Results;
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
        private readonly IOrderItemDal _orderItemDal;

        public OrderItemManager(IOrderItemDal orderItemDal)
        {
            _orderItemDal = orderItemDal;
        }

        public IDataResult<OrderItem> GetById(int id)
        {
            var item = _orderItemDal.Get(i => i.Id == id);

            if (item == null)
                return new ErrorDataResult<OrderItem>("OrderItem bulunamadı.");

            return new SuccessDataResult<OrderItem>(item);
        }

        public IDataResult<List<OrderItem>> GetByOrderId(int orderId)
        {
            var items = _orderItemDal.GetAll(i => i.OrderId == orderId);
            return new SuccessDataResult<List<OrderItem>>(items);
        }

        public IDataResult<List<OrderItem>> GetByProductId(int productId)
        {
            var items = _orderItemDal.GetAll(i => i.ProductId == productId);
            return new SuccessDataResult<List<OrderItem>>(items);
        }

        public IDataResult<OrderItem> GetByOrderAndProduct(int orderId, int productId)
        {
            var item = _orderItemDal.Get(i =>
                i.OrderId == orderId && i.ProductId == productId);

            if (item == null)
                return new ErrorDataResult<OrderItem>("OrderItem bulunamadı.");

            return new SuccessDataResult<OrderItem>(item);
        }
    }
}
