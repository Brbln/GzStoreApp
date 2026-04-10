using Core.Utilities.Results;
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
        IDataResult<OrderItem> GetById(int id);
        IDataResult<List<OrderItem>> GetByOrderId(int orderId);
        IDataResult<List<OrderItem>> GetByProductId(int productId);
        IDataResult<OrderItem> GetByOrderAndProduct(int orderId, int productId);
    }
}
