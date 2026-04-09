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
    public class EfOrderItemDal : EfRepositoryBase<OrderItem>, IOrderItemDal
    {
        public EfOrderItemDal(GamzeDbContext context) : base(context)
        {
        }

        public OrderItem GetByOrderAndProduct(int orderId, int productId)
        {
            return _context.OrderItems.FirstOrDefault(o => o.OrderId == orderId && o.ProductId == productId);
        }

        public List<OrderItem> GetByOrderId(int orderId)
        {
            return _context.OrderItems
                    .Where(o => o.OrderId == orderId).ToList();
        }

        public List<OrderItem> GetByProductId(int productId)
        {
            return _context.OrderItems
                    .Include(p => p.Product)
                    .Include(o => o.Order)
                    .Where(o => o.ProductId == productId).ToList();
        }
    }
}
