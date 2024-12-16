
using Business.Abstract;
using DataAccess.Abstract;
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
        ICartItemDal _cItemDal;

        public CartItemManager(ICartItemDal cItemDal)
        {
            _cItemDal = cItemDal;
        }

        public void Add(CartItem cart)
        {
            _cItemDal.Add(cart);
        }

        public void Delete(CartItem cart)
        {
            _cItemDal.Delete(cart);
        }

        public List<CartItem> GetAll()
        {
            return _cItemDal.GetAll();
        }

        public CartItem GetByCartAndProduct(int cartId, int productId)
        {
            return _cItemDal.Get(a => a.CartId == cartId && a.ProductId == productId);
        }

        public List<CartItem> GetByCartId(int cartId)
        {
            return _cItemDal.GetAll(a => a.CartId == cartId);
        }

        public CartItem GetById(int id)
        {
            return _cItemDal.Get(a => a.CartItemId == id);

        }

        public List<CartItem> GetByProductId(int productId)
        {
            return _cItemDal.GetAll(a => a.ProductId == productId);
        }

        public void Update(CartItem cart)
        {
            _cItemDal.Update(cart);
        }
    }
}
