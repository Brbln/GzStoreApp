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
    public class EfCartItemDal : EfRepositoryBase<CartItem>, ICartItemDal
    {
        public EfCartItemDal(GamzeDbContext context) : base(context)
        {
        }

        public CartItem GetByCartAndProduct(int cartId, int productId)
        {
            return _context.CartItems
                .Include(c=>c.Product).
                FirstOrDefault(c => c.CartId == cartId && c.ProductId == productId);
        }

        public List<CartItem> GetByCartId(int cartId)
        {
            return _context.CartItems
                .Include(c=>c.Product)
                .Where(c => c.CartId == cartId).ToList();
        }

        public List<CartItem> GetByProductId(int productId)
        {
            return _context.CartItems
                .Include(c=> c.Product)
                .Where(c => c.ProductId == productId).ToList();
        }
    }
}
