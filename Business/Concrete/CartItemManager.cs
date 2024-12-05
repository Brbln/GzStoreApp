using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartItemManager : ICartItemService
    {
        public void Add(CartItem cart)
        {
            throw new NotImplementedException();
        }

        public void Delete(CartItem cart)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public CartItem GetByCartAndProduct(int cartId, int productId)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetByCartId(int cartId)
        {
            throw new NotImplementedException();
        }

        public CartItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public void Update(CartItem cart)
        {
            throw new NotImplementedException();
        }
    }
}
