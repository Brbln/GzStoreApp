using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfRepositoryBase<Order>, IOrderDal
    {
        public EfOrderDal(GamzeDbContext context) : base(context)
        { }
        public List<Order> GetOrdersWithItems(Expression<Func<Order, bool>> filter = null)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }
    }
}

