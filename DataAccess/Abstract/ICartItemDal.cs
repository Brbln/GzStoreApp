using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICartItemDal : IEntityRepository<CartItem>
    {
          
        List<CartItem> GetByCartId(int cartId);         
        List<CartItem> GetByProductId(int productId);
        List<CartItem> GetAllWithProduct(Expression<Func<CartItem, bool>> filter);         
        CartItem GetByCartAndProduct(int cartId, int productId);
    }
}
