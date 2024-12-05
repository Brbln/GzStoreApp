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
        public void Add(CartItem cart);
        public void Update(CartItem cart);
        public void Delete(CartItem cart);
        public List<CartItem> GetAll();
        CartItem GetById(int id);
        List<CartItem> GetByCartId(int cartId);
        List<CartItem> GetByProductId(int productId);
        CartItem GetByCartAndProduct(int cartId, int productId);
    }
}
