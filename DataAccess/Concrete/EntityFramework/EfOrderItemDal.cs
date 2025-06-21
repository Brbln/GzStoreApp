using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
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
            using var context = new GamzeDbContext();
            return context.OrderItems.FirstOrDefault(o => o.OrderId == orderId && o.ProductId == productId);
        }

        public List<OrderItem> GetByOrderId(int orderId)
        {
            using var context = new GamzeDbContext();
            return context.OrderItems
                    .Where(o => o.OrderId == orderId).ToList();
        }

        public List<OrderItem> GetByProductId(int productId)
        {
            using var context = new GamzeDbContext();
            return context.OrderItems
                    .Include(p => p.Product)
                    .Include(o => o.Order)
                    .Where(o => o.ProductId == productId).ToList();
        }
    }
}
