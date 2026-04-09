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
    public class EfCartDal : EfRepositoryBase<Cart>, ICartDal
    {
        public EfCartDal(GamzeDbContext context) : base(context)
        {
        }

        public Cart GetByCartId(int cartId)
        {
            return _context.Carts
                .Include(c=>c.CartItems)
                .SingleOrDefault(c=>c.Id == cartId);
        }

        public Cart GetByUserId(int userId)
        {
            return _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefault(c => c.UserId == userId);
        }
    }
}
