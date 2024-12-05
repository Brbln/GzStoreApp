using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICartItemDal : IEntityRepository<CartItem>
    {

        // Belirli bir sepet ID'sine ait tüm sepet öğelerini getirir
        List<CartItem> GetByCartId(int cartId);

        // Belirli bir ürün ID'sine ait sepetteki öğeleri getirir
        List<CartItem> GetByProductId(int productId);

        // Sepet ID'si ve ürün ID'sine göre sepet öğesini getirir
        CartItem GetByCartAndProduct(int cartId, int productId);
    }
}
