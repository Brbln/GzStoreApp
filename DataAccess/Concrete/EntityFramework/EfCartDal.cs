using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCartDal : EfRepositoryBase<Cart, GamzeDbContext>, ICartDal
    {
        public Cart GetByCartId(int cartId)
        {
            return Get(c => c.CartId == cartId);
        }

        public Cart GetByUserId(int userId)
        {
            using var context = new GamzeDbContext();
            return context.Carts.FirstOrDefault(c => c.UserId == userId);
        }
    }
}
