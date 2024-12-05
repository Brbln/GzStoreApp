using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICartDal : IEntityRepository<Cart>
    {
        // Kullanıcıya ait sepeti getirir
        Cart GetByUserId(int userId);

        // Sepet ID'sine göre sepeti getirir
        Cart GetByCartId(int cartId);
    }
}
