using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderItemDal : EfRepositoryBase<OrderItem, GamzeDbContext>, IOrderItemDal
    {
        public OrderItem GetByOrderAndProduct(int orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public List<OrderItem> GetByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<OrderItem> GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
