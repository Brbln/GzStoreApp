using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCartDal : EfRepositoryBase<Cart, GamzeDbConcext>, ICartDal
    {
        public Cart GetByCartId(int cartId)
        {
            throw new NotImplementedException();
        }

        public Cart GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
