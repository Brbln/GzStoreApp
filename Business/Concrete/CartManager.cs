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
    public class CartManager : ICartService
    {
        ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void Add(Cart cart)
        {
            _cartDal.Add(cart);
        }

        public void Delete(Cart cart)
        {
            _cartDal.Delete(cart);
        }

        public List<Cart> GetAll()
        {
            return _cartDal.GetAll();
        }
         
        public Cart GetById(int id)
        {
            return _cartDal.Get(a => a.CartId == id);

        }

        public Cart GetByUserId(int userId)
        {
            return _cartDal.Get(a => a.UserId == userId);
        }

        public void Update(Cart cart)
        {
            _cartDal.Update(cart);
        }
    }
}
