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
    public class EfCartItemDal : EfRepositoryBase<CartItem>, ICartItemDal
    {
        public EfCartItemDal(GamzeDbContext context) : base(context)
        {
        }

        public CartItem GetByCartAndProduct(int cartId, int productId)
        {
            return _context.CartItems
                .Include(c=>c.Product)
                .ThenInclude(p => p.Images)
                .FirstOrDefault(c => c.CartId == cartId && c.ProductId == productId);
        }

        public List<CartItem> GetByCartId(int cartId)
        {
            return _context.CartItems
                .Include(c=>c.Product)
                .ThenInclude(p => p.Images)
                .Where(c => c.CartId == cartId).ToList();
        }

        public List<CartItem> GetByProductId(int productId)
        {
            return _context.CartItems
                .Include(c=> c.Product)
                .ThenInclude(p => p.Images)
                .Where(c => c.ProductId == productId).ToList();
        }
        public List<CartItem> GetAllWithProduct(Expression<Func<CartItem, bool>> filter)
        {
            return _context.CartItems.Include(c => c.Product)
                .ThenInclude(p => p.Images).Where(filter).ToList();
        }
    }
}
