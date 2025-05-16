using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartItemService
    {
        void Add(CartItem cart);
        void Update(CartItem cart);
        void Delete(CartItem cart);
        List<CartItem> GetAll();
        CartItem GetById(int id);
        List<CartItem> GetByCartId(int cartId);
        List<CartItem> GetByProductId(int productId);
        CartItem GetByCartAndProduct(int cartId, int productId);
        void AddOrUpdate(CartItem cart);

    }
}
