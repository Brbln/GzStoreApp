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
            throw new NotImplementedException();
        }

        public List<CartItem> GetByCartId(int cartId)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
