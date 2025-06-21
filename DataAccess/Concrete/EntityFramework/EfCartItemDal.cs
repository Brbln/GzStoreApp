using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCartItemDal : EfRepositoryBase<CartItem, GamzeDbContext>, ICartItemDal
    {
        public CartItem GetByCartAndProduct(int cartId, int productId)
        {
            using var context = new GamzeDbContext();
            return context.CartItems.FirstOrDefault(c => c.CartId == cartId && c.ProductId == productId);
        }

        public List<CartItem> GetByCartId(int cartId)
        {
            using var context = new GamzeDbContext();
            return context.CartItems.Where(c => c.CartId == cartId).ToList();
        }

        public List<CartItem> GetByProductId(int productId)
        {
            using var context = new GamzeDbContext();
            return context.CartItems.Where(c => c.ProductId == productId).ToList();
        }
    }
}
